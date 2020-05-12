using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;

namespace LearningEngine.Domain.Command
{
    public class CreateCardCommand : IRequest, IPipelinePermissionModel
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public TypeAccess Access { get ; private set ; }

        public CreateCardCommand(int userId, int themeId, string question, string answer, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Question = question;
            Answer = answer;
            Access = access;
        }
    }
}
