using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Domain.DTO
{
    public class ThemeHeaderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}
