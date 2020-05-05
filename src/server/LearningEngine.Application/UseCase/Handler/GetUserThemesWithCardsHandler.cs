using LearningEngine.Application.UseCase.Query;
using LearningEngine.Domain.Constants;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class GetUserThemesWithCardsHandler : BaseUseCaseHandler<List<ThemeDto>, GetUserThemesWithCardsQuery>
    {
        private readonly IMediator _mediator;

        public GetUserThemesWithCardsHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            _mediator = mediator;
        }

        protected override async Task<List<ThemeDto>> Action(GetUserThemesWithCardsQuery request)
        {
            var getThemeSubThemesQuery = new GetThemeSubThemesQuery(request.ThemeId, request.UserId);
            var themes = await _mediator.Send(getThemeSubThemesQuery);
            if(!themes.Any())
            {
                throw new Exception(ExceptionDescriptionConstants.SubThemesNotFound);
            }

            foreach(var theme in themes)
            {
                var getThemeCardsQuery = new GetThemeCardsQuery(theme.Id);
                theme.Cards = await _mediator.Send(getThemeCardsQuery);
            }

            return themes;
        }
    }
}
