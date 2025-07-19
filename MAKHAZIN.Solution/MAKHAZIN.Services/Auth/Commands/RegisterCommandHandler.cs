using MAKHAZIN.Core;
using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.Application.Features.Auth.Commands;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Entities.Identity;
using MAKHAZIN.Core.Services.Contract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<UserDTO>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
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

            var result = await _userManager.CreateAsync(AppUser,request.Password);

            if (!result.Succeeded)
                return Result<UserDTO>.Failure("Registeration failed :|");

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
