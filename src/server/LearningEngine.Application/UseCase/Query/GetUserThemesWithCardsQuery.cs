using LearningEngine.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Query
{
    public class GetUserThemesWithCardsQuery : IRequest<List<ThemeDto>>
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }

        public GetUserThemesWithCardsQuery(int userId, int themeId)
        {
            UserId = userId;
            ThemeId = themeId;
        }
    }
}
