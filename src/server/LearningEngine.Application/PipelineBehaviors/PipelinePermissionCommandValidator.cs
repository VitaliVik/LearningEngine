using LearningEngine.Application.Factories;
using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using LearningEngine.Domain.Query;
using MediatR;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LearningEngine.Application.PipelineBehaviors
{
    public class PipelinePermissionCommandValidator<TResponse>
        : IPipelineBehavior<IPipelinePermissionCommand, TResponse>
    {
        private readonly IMediator mediator;
        private readonly IGetPermissionModelFactory getPermissionModelFactory;

        public PipelinePermissionCommandValidator
              (IMediator mediator, IGetPermissionModelFactory getPermissionModelFactory)
        {
            this.mediator = mediator;
            this.getPermissionModelFactory = getPermissionModelFactory;
        }

        public async Task<TResponse> Handle(IPipelinePermissionCommand request,
                                        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var query = getPermissionModelFactory.GetModel
                        (request.ObjectId, request.UserId, TypeAccess.Write, request.ObjectType);

            await mediator.Send(query, cancellationToken);

            return await next();
        }
    }
}
