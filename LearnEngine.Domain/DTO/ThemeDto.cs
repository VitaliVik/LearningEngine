using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.DTO
{
    public class ThemeDto
    {
        public string Name { get; set; }
        public string Desription { get; set; }
        public bool IsPublic { get; set; }
        public ThemeDto ParentTheme { get; set; }
        public List<ThemeDto> SubThemes { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
