using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    public class Theme
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Theme> SubThemes { get; set; }

        public int? ParentThemeId { get; set; }

        public Theme ParentTheme { get; set; }

        public bool IsPublic { get; set; }

        public List<Card> Cards { get; set; }

        public List<Note> Notes { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
