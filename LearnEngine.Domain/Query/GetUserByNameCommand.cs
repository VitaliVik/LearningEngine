using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetUserByNameCommand : IRequest<UserDto>
    {
        public string UserName { get; private set; }
        public GetUserByNameCommand(string userName)
        {
            UserName = userName;
        }
    }
}
