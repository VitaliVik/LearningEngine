using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.Query;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;

namespace LearningEngine.Application.PipelineValidators
{
    public class PipelinePermissionQueryValidator<TResponse> : IPipelineBehavior<IPipelinePermissionQuery, TResponse>
    {
        private readonly IMediator _mediator;

        public PipelinePermissionQueryValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(IPipelinePermissionQuery request, 
                                        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var checkUserPermissionQuery = new CheckUserPermissionsQuery(request.UserId, 
                                                      request.ThemeId, TypeAccess.Read | TypeAccess.Write);

            await _mediator.Send(checkUserPermissionQuery, cancellationToken);

            return await next();
        }
    }
}
