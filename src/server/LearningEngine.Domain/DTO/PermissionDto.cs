using LearningEngine.Domain.Enum;

namespace LearningEngine.Domain.DTO
{
    public class PermissionDto
    {
        public string ThemeName { get; set; }

        public string UserName { get; set; }

        public TypeAccess Access { get; set; }
    }
}
