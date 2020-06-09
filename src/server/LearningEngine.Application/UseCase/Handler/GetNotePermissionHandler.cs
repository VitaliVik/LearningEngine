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
    public class GetNotePermissionHandler : BaseUseCaseHandler<Unit, GetNotePermissionQuery>
    {
        private readonly IMediator mediator;

        public GetNotePermissionHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            this.mediator = mediator;
        }

        protected override async Task<Unit> Action(GetNotePermissionQuery request)
        {
            var getThemeByNoteIdQuery = new GetThemeByNoteIdQuery(request.NoteId);

            var theme = await mediator.Send(getThemeByNoteIdQuery);

            var checkUserPermissionsQuery = new CheckUserThemePermissionsQuery(request.UserId, 
                                                                               theme.Id, 
                                                                               request.Access);

            await mediator.Send(checkUserPermissionsQuery);

            return default;
        }
    }
}
