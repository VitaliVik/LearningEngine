using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR.Pipeline;
using System.Linq;

namespace LearningEngine.Application.PipelineValidators
{
    public class PipelinePermissionValidator<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly IMediator _mediator;

        public PipelinePermissionValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            IPipelinePermissionModel model = (IPipelinePermissionModel)request;
            var checkUserPermissionQuery = new CheckUserPermissionsQuery(model.UserId, model.ThemeId, model.Access);
            await _mediator.Send(checkUserPermissionQuery, cancellationToken);
        }
    }
}
