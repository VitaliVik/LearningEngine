﻿using LearningEngine.Application.UseCase.Command;
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
        private readonly IMediator _mediator;

        public CreateCardAndStatisticHandler(IMediator mediator, ITransactionUnitOfWork uow) : base(uow)
        {
            _mediator = mediator;
        }

        protected async override Task<Unit> Action(CreateCardAndStatisticCommand request)
        {
            var createCardCommand = new CreateCardCommand(request.UserId, request.ObjectId, 
                                                          request.Question, request.Answer);
            var cardId = await _mediator.Send(createCardCommand);

            var createStatistic = new CreateStatisicCommand(request.UserId, cardId);
            await _mediator.Send(createStatistic);

            return default;
        }
    }
}
