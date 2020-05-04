using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.DTO
{
    public class ThemeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desсription { get; set; }
        public bool IsPublic { get; set; }
        public ThemeDto ParentTheme { get; set; }
        public List<ThemeDto> SubThemes { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
