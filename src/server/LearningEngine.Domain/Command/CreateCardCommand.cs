using FluentValidation;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class CreateCardCommand : IRequest<int>, IPipelinePermissionCommand
    {
        public int UserId { get; private set; }

        public int ThemeId { get; private set; }

        public string Question { get; private set; }

        public string Answer { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public int ObjectId => ThemeId;

        public CreateCardCommand(int userId, int themeId, string question, string answer)
        {
            UserId = userId;
            ThemeId = themeId;
            Question = question;
            Answer = answer;
        }
    }

    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(card => card.UserId).GreaterThan(0);
            RuleFor(card => card.ThemeId).GreaterThan(0);
            RuleFor(card => card.Question).NotNull().NotEmpty().MinimumLength(4);
            RuleFor(card => card.Answer).NotNull().NotEmpty().MinimumLength(4);
        }
    }
}
