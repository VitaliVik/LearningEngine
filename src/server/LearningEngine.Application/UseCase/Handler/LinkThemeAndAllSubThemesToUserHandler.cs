using LearningEngine.Application.UseCase.Command;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.DTO;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class LinkThemeAndAllSubThemesToUserHandler : BaseUseCaseHandler<Unit, LinkThemeAndAllSubThemesToUserCommand>
    {
        private readonly IMediator _mediator;

        public LinkThemeAndAllSubThemesToUserHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            _mediator = mediator;
        }
        protected async override Task<Unit> Action(LinkThemeAndAllSubThemesToUserCommand request)
        {
            List<ThemeDto> themes = new List<ThemeDto>();
            var getThemeHeaderQuery = new GetThemeHeaderQuery(request.ThemeId, request.UserId);
            themes.Add(await _mediator.Send(getThemeHeaderQuery));

            for (int i = 0; i < themes.Count; i++)
            {
                var getThemeSubThemes = new GetThemeSubThemesQuery(themes[i].Id, request.UserId);
                themes.AddRange(await _mediator.Send(getThemeSubThemes));
            }

            foreach (var theme in themes)
            {
                var linkUserToThemeCommand = new LinkUserToThemeCommand(request.UserId, theme.Id, request.Access);
                await _mediator.Send(linkUserToThemeCommand);
            }

            return default;
        }
    }
}
