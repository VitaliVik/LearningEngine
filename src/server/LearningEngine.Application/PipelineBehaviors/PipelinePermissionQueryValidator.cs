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
        private readonly IMediator mediator;
        private readonly IGetPermissionModelFactory getPermissionModelFactory;

        public PipelinePermissionQueryValidator(IMediator mediator,
                                                  IGetPermissionModelFactory getPermissionModelFactory)
        {
            this.mediator = mediator;
            this.getPermissionModelFactory = getPermissionModelFactory;
        }

        public async Task<TResponse> Handle(IPipelinePermissionQuery request,
                                            CancellationToken cancellationToken, 
                                            RequestHandlerDelegate<TResponse> next)
        {
            var query = getPermissionModelFactory.GetModel(request.ObjectId, 
                                                           request.UserId, 
                                                           TypeAccess.Read, 
                                                           request.ObjectType);

            await mediator.Send(query, cancellationToken);

            return await next();
        }
    }
}
