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
        private readonly IMediator _mediator;
        private readonly IGetPermissionModelFactory _getPermissionModelFactory;

        public PipelinePermissionCommandValidator(IMediator mediator, 
                                                  IGetPermissionModelFactory getPermissionModelFactory)
        {
            _mediator = mediator;
            _getPermissionModelFactory = getPermissionModelFactory;
        }

        public async Task<TResponse> Handle(IPipelinePermissionCommand request, 
                                        CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            int themeId;

            if(request.ObjectType == ObjectType.Theme)
            {
                themeId = request.ObjectId;
            }
            else
            {
                var query = _getPermissionModelFactory.GetModel(request.ObjectId, request.ObjectType);

                var theme = await _mediator.Send(query);
                themeId = theme.Id;
            }

            var checkUserPermissionQuery = new CheckUserPermissionsQuery(request.UserId, themeId, TypeAccess.Write);

            await _mediator.Send(checkUserPermissionQuery, cancellationToken);

            return await next();
        }
    }
}
