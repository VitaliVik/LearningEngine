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
}
