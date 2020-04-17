using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetRootThemesByUserIdQuery : IRequest<List<ThemeHeaderDto>>
    {
        public int UserId { get; set; }
        public GetRootThemesByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
