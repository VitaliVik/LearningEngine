using LearningEngine.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.Query
{
    public class CheckUserPermissionsQuery : IRequest
    {
        public int UserId { get; private set; }
        public int ThemeId { get; private set; }
        public TypeAccess Access { get; private set; }
        public CheckUserPermissionsQuery(int userId, int themeId, TypeAccess access)
        {
            UserId = userId;
            ThemeId = themeId;
            Access = access;
        }
    }
}
