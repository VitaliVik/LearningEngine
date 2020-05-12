using LearningEngine.Domain.Enum;
using LearningEngine.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class CreateNoteCommand: IRequest, IPipelinePermission
    {
        public CreateNoteCommand(int themeId, int userId, string title, string content, TypeAccess access)
        {
            ThemeId = themeId;
            UserId = userId;
            Title = title;
            Content = content;
            Access = access;
        }

        public int ThemeId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public int UserId { get; private set; }
        public TypeAccess Access { get; private set; }
    }
}
