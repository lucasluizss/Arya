﻿using System;
using Tyrion;

namespace Arya.Application.Domain.Commands
{
    public class UserBaseCommand : IRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
