using FluentValidation;
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

    public class GetThemeByCardIdQueryValidator : AbstractValidator<GetThemeByCardIdQuery>
    {
        public GetThemeByCardIdQueryValidator()
        {
            RuleFor(card => card.CardId).GreaterThan(0);
        }
    }
}
