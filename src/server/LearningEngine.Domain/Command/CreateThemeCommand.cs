using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class CreateThemeCommand : IRequest<int>
    {
        public CreateThemeCommand(string themeName, string description, bool isPublic, int? parentThemeId = null)
        {
            ThemeName = themeName;
            Description = description;
            IsPublic = isPublic;
            ParentThemeId = parentThemeId;
        }

        public string ThemeName { get; }

        public string Description { get; }

        public bool IsPublic { get; }

        public int? ParentThemeId { get; }
    }

    public class CreateThemeCommandValidator : AbstractValidator<CreateThemeCommand>
    {
        public CreateThemeCommandValidator()
        {
            RuleFor(theme => theme.ThemeName).NotNull().NotEmpty();
            RuleFor(theme => theme.Description).NotNull().NotEmpty().MinimumLength(4);
        }
    }
}
