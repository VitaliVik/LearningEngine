using FluentValidation;
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

        public int CardId { get; set; }

        public double Value { get; set; }

        public ObjectType ObjectType => ObjectType.Card;

        public int ObjectId => CardId;

        public EditUserKnowledgeCommand(int userId, int cardId, double value)
        {
            UserId = userId;
            CardId = cardId;
            Value = value;
        }
    }

    public class EditUserKnowledgeCommandValidator : AbstractValidator<EditUserKnowledgeCommand>
    {
        public EditUserKnowledgeCommandValidator()
        {
            RuleFor(statistic => statistic.CardId).GreaterThan(0);
            RuleFor(statistic => statistic.UserId).GreaterThan(0);
            RuleFor(statistic => statistic.Value).NotEqual(0);
        }
    }
}
