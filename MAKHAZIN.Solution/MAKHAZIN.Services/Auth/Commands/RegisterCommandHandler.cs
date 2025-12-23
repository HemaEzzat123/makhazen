using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Auth.Commands;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Enums;
using MAKHAZIN.Core.Services.Contract;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MAKHAZIN.Services.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UserDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        public async Task<Result<UserDTO>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if email exists

            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result<UserDTO>.Failure("This Email is already in use :(");

            // Craete New User

            var AppUser = new AppUser
            {
                Name = request.DisplayName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                UserName = request.Email.Split('@')[0],
                IsActive = true
            };

            // Validate The role

            var validRoles = new[] { UserRoles.Admin, UserRoles.Buyer, UserRoles.Seller };

            if (!validRoles.Contains(request.Role))
                return Result<UserDTO>.Failure("Invalid Role Selected.");

            // Create the User with the userManager and validate
            var result = await _userManager.CreateAsync(AppUser,request.Password);

            if (!result.Succeeded)
                return Result<UserDTO>.Failure("Registeration failed :|");

            // Asign Role

            var RoleResult = await _userManager.AddToRoleAsync(AppUser, request.Role);
            if (!RoleResult.Succeeded)
            {
                var errors = string.Join(", ", RoleResult.Errors.Select(e => e.Description));
                return Result<UserDTO>.Failure($"Failed To Assign Role: {errors}");
            }

            var token = await _tokenService.CreateTokenAsync(AppUser, _userManager);
            var user = new User
            {
                ExternalId = AppUser.Id,
                Name = AppUser.Name,
                Address = AppUser.Address,
                IsActive = AppUser.IsActive ?? true,
            };
            var userDto = new UserDTO
            {
                DisplayName = request.DisplayName,
                Email = request.Email,
                Token = token,
            };

             await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return Result<UserDTO>.Success(userDto);
        }
    }
}
