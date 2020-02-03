using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Application.DTO
{
    public class CardDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public ThemeDto Theme { get; set; }
    }
}
