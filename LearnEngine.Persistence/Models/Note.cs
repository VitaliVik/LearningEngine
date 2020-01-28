using System;
using System.Collections.Generic;
using System.Text;

namespace LearnEngine.Persistence.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }

        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
    }
}
