using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class RegisterUserCommand : IRequest
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

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(user => user.UserName).NotNull().NotEmpty().MinimumLength(3);
            RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(user => user.Password).NotNull().NotEmpty().MinimumLength(4);
        }
    }
}
