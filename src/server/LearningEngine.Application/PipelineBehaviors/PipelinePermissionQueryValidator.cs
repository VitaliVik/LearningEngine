using MediatR;
using System.Threading;
using System.Threading.Tasks;
using LearningEngine.Domain.Query;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using LearningEngine.Application.Factories;

namespace LearningEngine.Application.PipelineValidators
{
    public class PipelinePermissionQueryValidator<TResponse> : IPipelineBehavior<IPipelinePermissionQuery, TResponse>
    {
        private readonly IMediator _mediator;
        private readonly IGetPermissionModelFactory _getPermissionModelFactory;

        public PipelinePermissionQueryValidator(IMediator mediator,
                                                  IGetPermissionModelFactory getPermissionModelFactory)
        {
            _mediator = mediator;
            _getPermissionModelFactory = getPermissionModelFactory;
        }

        public async Task<TResponse> Handle(IPipelinePermissionQuery request,
                                        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var query = _getPermissionModelFactory.GetModel
            (request.ObjectId, request.UserId, TypeAccess.Read, request.ObjectType);

            await _mediator.Send(query, cancellationToken);

            return await next();
        }
    }
}
