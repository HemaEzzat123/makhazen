using MAKHAZIN.Core;
using MAKHAZIN.Application.CQRS;
using MAKHAZIN.Application.Features.Users.Command;
using MAKHAZIN.Core.DTOs;
using MAKHAZIN.Core.Entities;
using MAKHAZIN.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Services.Users.Command
{
    public class UpdateProfileCommandHandler : ICommandHandler<UpdateProfileCommand, UserToReturnDTO>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileCommandHandler(UserManager<AppUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UserToReturnDTO>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is null)
                return Result<UserToReturnDTO>.Failure("User not found");

            // Update Fields

            user.Name = request.Name ?? user.Name;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            user.Address = request.Address ?? user.Address;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<UserToReturnDTO>.Failure("Failed to update profile");
            }
            var dbUser = await _unitOfWork.Repository<User>().FindFirstOrDefaultAsync(u => u.ExternalId == request.UserId);
            if (dbUser != null)
            {
                dbUser.Name = request.Name ?? dbUser.Name;
                dbUser.Address = request.Address ?? dbUser.Address;
                _unitOfWork.Repository<User>().Update(dbUser);
                await _unitOfWork.CompleteAsync();
            }
            var userDto = new UserToReturnDTO
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address ?? string.Empty
            };
            return Result<UserToReturnDTO>.Success(userDto);
        }
    }
}
