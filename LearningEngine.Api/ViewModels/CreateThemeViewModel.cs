using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningEngine.Api.ViewModels
{
    public class CreateThemeViewModel
    {
        public string UserName { get; set; }
        public string ThemeName { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public int? ParentThemeId { get; set; }
    }
}
