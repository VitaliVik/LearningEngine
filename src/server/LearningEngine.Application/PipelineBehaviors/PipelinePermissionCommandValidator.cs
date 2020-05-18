using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using LearningEngine.Domain.Query;
using MediatR;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.PipelineBehaviors
{
    public class PipelinePermissionCommandValidator<TResponse> 
        : IPipelineBehavior<IPipelinePermissionCommand, TResponse>
    {
        private readonly IMediator _mediator;

        public PipelinePermissionCommandValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> Handle(IPipelinePermissionCommand request, 
                                        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var checkUserPermissionQuery = new CheckUserPermissionsQuery(request.UserId,
                                                      request.ThemeId, TypeAccess.Write);

            await _mediator.Send(checkUserPermissionQuery, cancellationToken);

            return await next();
        }
    }
}
