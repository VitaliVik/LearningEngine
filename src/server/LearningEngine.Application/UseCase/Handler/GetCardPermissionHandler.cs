using LearningEngine.Application.UseCase.Query;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class GetCardPermissionHandler : BaseUseCaseHandler<Unit, GetCardPermissionQuery>
    {
        private readonly IMediator mediator;

        public GetCardPermissionHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            this.mediator = mediator;
        }

        protected override async Task<Unit> Action(GetCardPermissionQuery request)
        {
            var getThemeByCardIdQuery = new GetThemeByCardIdQuery(request.CardId);

            var theme = await mediator.Send(getThemeByCardIdQuery);

            var checkUserPermissionsQuery = new CheckUserThemePermissionsQuery(request.UserId,
                                                                               theme.Id, 
                                                                               request.Access);

            await mediator.Send(checkUserPermissionsQuery);

            return default;
        }
    }
}
