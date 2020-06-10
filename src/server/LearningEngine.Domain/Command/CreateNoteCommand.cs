using FluentValidation;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class CreateNoteCommand : IRequest, IPipelinePermissionCommand
    {
        public CreateNoteCommand(int themeId, int userId, string title, string content)
        {
            ThemeId = themeId;
            UserId = userId;
            Title = title;
            Content = content;
        }

        public int ThemeId { get; private set; }

        public string Title { get; private set; }

        public string Content { get; private set; }

        public int UserId { get; private set; }

        public ObjectType ObjectType => ObjectType.Theme;

        public int ObjectId => ThemeId;
    }

    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(note => note.UserId).GreaterThan(0);
            RuleFor(note => note.ThemeId).GreaterThan(0);
            RuleFor(note => note.Title).NotNull().NotEmpty().MinimumLength(4);
            RuleFor(note => note.Content).NotNull().NotEmpty().MinimumLength(4);
        }
    }
}
