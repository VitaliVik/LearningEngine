using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class CreateNoteCommand: IRequest
    {
        public CreateNoteCommand(int themeId, string title, string content)
        {
            ThemeId = themeId;
            Title = title;
            Content = content;
        }

        public int ThemeId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
    }
}
