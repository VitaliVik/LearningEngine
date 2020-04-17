using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.DTO
{
    public class ThemeHeaderDto
    {
        public string Name { get; set; }
        public string Desription { get; set; }
        public bool IsPublic { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
