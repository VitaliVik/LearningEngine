using MediatR;

namespace LearningEngine.Domain.Command
{
    public class CreateCardCommand : IRequest
    {
        public int UserId { get; set; }
        public int ThemeId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public CreateCardCommand(int userId, int themeId, string question, string answer)
        {
            UserId = userId;
            ThemeId = themeId;
            Question = question;
            Answer = answer;
        }
    }
}
