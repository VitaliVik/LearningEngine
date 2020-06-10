using FluentValidation;
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

        public class GetThemeByStatisticIdQueryValidator : AbstractValidator<GetThemeByStatisticIdQuery>
        {
            public GetThemeByStatisticIdQueryValidator()
            {
                RuleFor(statistic => statistic.StatisticId).GreaterThan(0);
            }
        }
    }
}
