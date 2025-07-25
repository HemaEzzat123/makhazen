﻿using MAKHAZIN.Core.Application.CQRS;
using MAKHAZIN.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAKHAZIN.Core.Application.Features.Auth.Commands
{
    public class RegisterCommand : ICommand<UserDTO>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
    }
}
