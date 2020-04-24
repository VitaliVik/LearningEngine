using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Command
{
    public class DeleteThemeCommand : IRequest
    {
        public DeleteThemeCommand(int themeId, int userId)
        {
            ThemeId = themeId;
            UserId = userId;
        }
        public int ThemeId { get; private set; }
        public int UserId { get; private set; }
    }
}
