using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR.Pipeline;
using System.Linq;

namespace LearningEngine.Application.PipelineValidators
{
    public class PipelinePermissionValidator<TResponse> : IPipelineBehavior<IPipelinePermissionModel, TResponse>
    {
        private readonly IMediator _mediator;

        public PipelinePermissionValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(IPipelinePermissionModel request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var checkUserPermissionQuery = new CheckUserPermissionsQuery(request.UserId, request.ThemeId, request.Access);

            await _mediator.Send(checkUserPermissionQuery, cancellationToken);

            return await next();
        }
    }
}
