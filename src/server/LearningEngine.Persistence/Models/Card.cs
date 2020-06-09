using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    public class Card
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public int ThemeId { get; set; }

        public Theme Theme { get; set; }
    }
}
