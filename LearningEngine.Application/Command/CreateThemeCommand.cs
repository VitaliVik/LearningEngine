using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.Command
{
    public class CreateThemeCommand : IRequest<bool>
    {
        public CreateThemeCommand(string userName, string themeName, string description, bool isPublic, int? parentThemeId = null)
        {
            UserName = userName;
            ThemeName = themeName;
            Description = description;
            IsPublic = isPublic;
            ParentThemeId = parentThemeId;
        }

        public string UserName { get; }
        public string ThemeName { get; }
        public string Description { get; }
        public bool IsPublic { get; }
        public int? ParentThemeId { get; }

    }
}
