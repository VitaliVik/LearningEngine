using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces.PipelinePermissions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class EditUserKnowledgeCommand : IRequest, IPipelinePermissionCommand
    {
        public int UserId { get; set; }

        public int ObjectId { get; set; }

        public double Value { get; set; }

        public ObjectType ObjectType => ObjectType.Card;

        public EditUserKnowledgeCommand(int userId, int cardId, double value)
        {
            UserId = userId;
            ObjectId = cardId;
            Value = value;
        }
    }
}
