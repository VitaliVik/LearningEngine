using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class RegisterUserCommand: IRequest
    {
        public RegisterUserCommand(string userName, string email, string password)
        {
            UserName = userName;
            Email = email;
            Password = password;
        }

        public string UserName { get; }
        public string Email { get; }
        public string Password { get; }
    }
}
