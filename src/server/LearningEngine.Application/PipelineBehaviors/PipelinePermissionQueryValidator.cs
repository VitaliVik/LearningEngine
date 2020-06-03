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
            int themeId;

            if (request.ObjectType == ObjectType.Theme)
            {
                themeId = request.ObjectId;
            }
            else
            {
                var query = _getPermissionModelFactory.GetModel(request.ObjectId, request.ObjectType);

                var theme = await _mediator.Send(query);
                themeId = theme.Id;
            }

            var checkUserPermissionQuery = new CheckUserPermissionsQuery(request.UserId, themeId, TypeAccess.Read);

            await _mediator.Send(checkUserPermissionQuery, cancellationToken);

            return await next();
        }
    }
}
