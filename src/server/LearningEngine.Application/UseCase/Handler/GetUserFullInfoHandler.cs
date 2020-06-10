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
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class GetUserFullInfoHandler : BaseUseCaseHandler<ThemeDto, GetThemeFullInfoQuery>
    {
        private readonly IMediator mediator;

        public GetUserFullInfoHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            this.mediator = mediator;
        }

        protected override async Task<ThemeDto> Action(GetThemeFullInfoQuery request)
        {
            var getThemeHeaderQuery = new GetThemeHeaderQuery(request.ThemeId, request.UserId);
            var theme = await mediator.Send(getThemeHeaderQuery);

            var getThemeCardsQuery = new GetThemeCardsQuery(request.ThemeId, request.UserId);
            theme.Cards = await mediator.Send(getThemeCardsQuery);

            var getThemeNotesQuery = new GetThemeNotesQuery(request.ThemeId, request.UserId);
            theme.Notes = await mediator.Send(getThemeNotesQuery, CancellationToken.None);

            var getThemeSubThemesQuery = new GetThemeSubThemesQuery(request.ThemeId, request.UserId);
            theme.SubThemes = await mediator.Send(getThemeSubThemesQuery);

            return theme;
        }
    }
}
