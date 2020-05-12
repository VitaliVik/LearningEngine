using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR.Pipeline;

namespace LearningEngine.Application.PipelineValidators
{
    public class PipelinePermissionValidator<TRequest> : IRequestPreProcessor<TRequest> where TRequest : IPipelinePermissionModel
    {
        private readonly IMediator _mediator;

        public PipelinePermissionValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var checkUserPermissionQuery = new CheckUserPermissionsQuery(request.UserId, request.ThemeId, request.Access);
            await _mediator.Send(checkUserPermissionQuery, cancellationToken);
        }
    }
}
