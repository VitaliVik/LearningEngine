using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class CreateNoteCommand: IRequest
    {
        public CreateNoteCommand(string themeName, string title, string content)
        {
            ThemeName = themeName;
            Title = title;
            Content = content;
        }

        public string ThemeName { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
    }
}
