using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Command
{
    public class CreateCardAndStatisticCommand : IRequest, IPipelinePermissionModel
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }
        public string Question { get; private set; }
        public string Answer { get; private set; }
        public TypeAccess Access { get; private set; }

        public CreateCardAndStatisticCommand(int userId, int themeId, string question, string answer, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Question = question;
            Answer = answer;
            Access = access;
        }
    }
}
