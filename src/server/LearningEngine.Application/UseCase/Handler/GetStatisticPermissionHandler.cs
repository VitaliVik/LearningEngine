using LearningEngine.Application.UseCase.Query;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class GetStatisticPermissionHandler : BaseUseCaseHandler<Unit, GetStatisticPermissionQuery>
    {
        private readonly IMediator _mediator;

        public GetStatisticPermissionHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            _mediator = mediator;
        }

        protected override async Task<Unit> Action(GetStatisticPermissionQuery request)
        {
            var getThemeByStatisticIdQuery = new GetThemeByStatisticIdQuery(request.StatisticId);

            var theme = await _mediator.Send(getThemeByStatisticIdQuery);

            var checkUserPermissionsQuery = new CheckUserThemePermissionsQuery
                                                (request.UserId, theme.Id, request.Access);

            await _mediator.Send(checkUserPermissionsQuery);

            return default;
        }
    }
}
