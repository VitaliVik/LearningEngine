using FluentValidation;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class EditThemeCommand : IRequest, IPipelinePermissionCommand
    {
        public ThemeDto ThemeDto { get; private set; }

        public int UserId { get; private set; }

        public int ThemeId { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public int ObjectId => ThemeId;

        public EditThemeCommand(ThemeDto themeDto, int userId, int objectId)
        {
            ThemeDto = themeDto;
            UserId = userId;
            ThemeId = objectId;
        }
    }

    public class EditThemeCommandValidator : AbstractValidator<EditThemeCommand>
    {
        public EditThemeCommandValidator()
        {
            RuleFor(theme => theme.ThemeId).GreaterThan(0);
            RuleFor(theme => theme.UserId).GreaterThan(0);
            RuleFor(theme => theme.ThemeDto).NotNull().NotEmpty();
            RuleFor(theme => theme.ThemeDto.Desсription).NotNull().NotEmpty().MinimumLength(4);
            RuleFor(theme => theme.ThemeDto.Name).NotNull().NotEmpty().MinimumLength(4);
        }
    }
}
