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

        public int ThemeId { get; set; }

        public int CardId { get; set; }

        public double Value { get; set; }

        public EditUserKnowledgeCommand(int userId, int themeId, int cardId, double value)
        {
            UserId = userId;
            ThemeId = themeId;
            CardId = cardId;
            Value = value;
        }
    }
}
