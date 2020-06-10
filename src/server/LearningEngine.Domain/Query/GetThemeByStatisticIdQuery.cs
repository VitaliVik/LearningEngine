using LearningEngine.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
{
    public class GetThemeByStatisticIdQuery : IRequest<ThemeDto>
    {
        public int StatisticId { get; set; }

        public GetThemeByStatisticIdQuery(int statisticId)
        {
            StatisticId = statisticId;
        }
    }
}
