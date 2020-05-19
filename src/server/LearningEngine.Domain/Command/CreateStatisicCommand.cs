using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class CreateStatisicCommand : IRequest
    {
        public int UserId { get; private set; }


        public int CardId { get; private set; }

        public CreateStatisicCommand(int userId, int cardId)
        {
            UserId = userId;
            CardId = cardId;
        }
    }
}
