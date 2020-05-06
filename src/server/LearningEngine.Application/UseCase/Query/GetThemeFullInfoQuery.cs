using LearningEngine.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetThemeFullInfoQuery : IRequest<ThemeDto>
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }

        public GetThemeFullInfoQuery(int userId, int themeId)
        {
            UserId = userId;
            ThemeId = themeId;
        }
    }
}
