using LearningEngine.Domain.Enum;
using MediatR;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetCardPermissionQuery : IRequest
    {
        public int CardId { get; private set; }

        public int UserId { get; private set; }

        public TypeAccess Access { get; private set; }

        public GetCardPermissionQuery(int cardId, TypeAccess access, int userId)
        {
            CardId = cardId;
            Access = access;
            UserId = userId;
        }
    }
}
