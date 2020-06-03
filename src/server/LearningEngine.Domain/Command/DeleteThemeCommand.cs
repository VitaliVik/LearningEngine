using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class DeleteThemeCommand : IRequest, IPipelinePermissionCommand
    {
        public DeleteThemeCommand(int objectId, int userId)
        {
            ObjectId = objectId;
            UserId = userId;
            ObjectType = ObjectType.Theme;
        }
        public int ObjectId { get; private set; }
        public int UserId { get; private set; }

        public ObjectType ObjectType { get; private set; }
    }
}
