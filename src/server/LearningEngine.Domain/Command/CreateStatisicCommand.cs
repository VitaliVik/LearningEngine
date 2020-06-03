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

        public int ObjectId { get; private set; }

        public ObjectType ObjectType => ObjectType.Card;

        public CreateStatisicCommand(int userId, int cardId)
        {
            UserId = userId;
            ObjectId = cardId;
        }
    }
}
