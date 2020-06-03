using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class CreateCardCommand : IRequest<int>, IPipelinePermissionCommand
    {
        public int UserId { get; private set; }
        public int ObjectId { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }

        public ObjectType ObjectType { get; private set; }

        public CreateCardCommand(int userId, int objectId, string question, string answer)
        {
            UserId = userId;
            ObjectId = objectId;
            Question = question;
            Answer = answer;
            ObjectType = ObjectType.Theme;
        }
    }
}
