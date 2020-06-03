using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class CreateStatisicCommand : IRequest, IPipelinePermissionCommand
    {
        public int UserId { get; private set; }

        public int CardId { get; private set; }

        public ObjectType ObjectType => ObjectType.Card;
        public int ObjectId => CardId;

        public CreateStatisicCommand(int userId, int cardId)
        {
            UserId = userId;
            CardId = cardId;
        }
    }
}
