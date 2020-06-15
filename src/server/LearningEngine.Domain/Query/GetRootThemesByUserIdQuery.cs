using FluentValidation;
using LearningEngine.Domain.DTO;
using MediatR;
using System.Collections.Generic;

namespace LearningEngine.Domain.Query
{
    public class GetRootThemesByUserIdQuery : IRequest<List<ThemeHeaderDto>>
    {
        public int UserId { get; private set; }

        public GetRootThemesByUserIdQuery(int userId)
        {
            UserId = userId;
        }

        public class GetRootThemesByUserIdQueryValidator : AbstractValidator<GetRootThemesByUserIdQuery>
        {
            public GetRootThemesByUserIdQueryValidator()
            {
                RuleFor(rootThemes => rootThemes.UserId).GreaterThan(0);
            }
        }
    }
}
