using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.DTO
{
    public class UserDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public List<PermissionDto> Permissions { get; set; }
    }
}
