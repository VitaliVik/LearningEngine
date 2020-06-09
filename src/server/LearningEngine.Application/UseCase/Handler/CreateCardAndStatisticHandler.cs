using LearningEngine.Application.UseCase.Command;
using LearningEngine.Domain.Command;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearningEngine.Application.UseCase.Handler
{
    public class CreateCardAndStatisticHandler : BaseUseCaseHandler<Unit, CreateCardAndStatisticCommand>
    {
        private readonly IMediator mediator;

        public CreateCardAndStatisticHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            this.mediator = mediator;
        }

        protected async override Task<Unit> Action(CreateCardAndStatisticCommand request)
        {
            var createCardCommand = new CreateCardCommand(request.UserId, 
                                                          request.ThemeId,
                                                          request.Question, 
                                                          request.Answer);
            var cardId = await mediator.Send(createCardCommand);

            var createStatistic = new CreateStatisicCommand(request.UserId, 
                                                            cardId);
            await mediator.Send(createStatistic);

            return default;
        }
    }
}
