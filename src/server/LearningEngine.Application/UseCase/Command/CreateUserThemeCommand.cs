using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.UseCase.Command
{
    public class CreateUserThemeCommand: IRequest
    {
        public CreateUserThemeCommand(string userName, string themeName, string description, 
                                      bool isPublic, int userId, int? parentThemeId = null)
        {
            UserName = userName;
            ThemeName = themeName;
            Description = description;
            IsPublic = isPublic;
            ParentThemeId = parentThemeId;
            UserId = userId;
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int? ParentThemeId { get; set; }
    }
}
