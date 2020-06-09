using LearningEngine.Domain.DTO;
using MediatR;

namespace LearningEngine.Domain.Query
{
    public class GetUserByNameQuery : IRequest<UserDto>
    {
        public string UserName { get; private set; }

        public GetUserByNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
