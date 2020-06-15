using FluentValidation;
using LearningEngine.Domain.Enum;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class LinkUserToThemeCommand : IRequest
    {
        public int UserId { get; private set; }

        public int ThemeId { get; private set; }

        public TypeAccess Access { get; private set; }

        public LinkUserToThemeCommand(int userId, int themeId, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Access = access;
        }
    }

    public class LinkUserToThemeCommandValidator : AbstractValidator<LinkUserToThemeCommand>
    {
        public LinkUserToThemeCommandValidator()
        {
            RuleFor(theme => theme.ThemeId).GreaterThan(0);
            RuleFor(theme => theme.UserId).GreaterThan(0);
        }
    }
}
