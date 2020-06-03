using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetThemeByCardIdQuery : IRequest<ThemeDto>
    {
        public int CardId { get; set; }

        public GetThemeByCardIdQuery(int cardId)
        {
            CardId = cardId;
        }
    }
}
