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
        private readonly IMediator _mediator;

        public GetUserFullInfoHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            _mediator = mediator;
        }

        protected override async Task<ThemeDto> Action(GetThemeFullInfoQuery request)
        {
            var getThemeHeaderQuery = new GetThemeHeaderQuery(request.ObjectId, request.UserId);
            var theme = await _mediator.Send(getThemeHeaderQuery);

            var getThemeCardsQuery = new GetThemeCardsQuery(request.ObjectId, request.UserId);
            theme.Cards = await _mediator.Send(getThemeCardsQuery);

            var getThemeNotesQuery = new GetThemeNotesQuery(request.ObjectId, request.UserId);
            theme.Notes = await _mediator.Send(getThemeNotesQuery, CancellationToken.None);

            var getThemeSubThemesQuery = new GetThemeSubThemesQuery(request.ObjectId, request.UserId);
            theme.SubThemes = await _mediator.Send(getThemeSubThemesQuery);

            return theme;
        }
    }
}
