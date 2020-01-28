using System;
using System.Collections.Generic;
using System.Text;


namespace LearnEngine.Persistence.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public int ThemeId { get; set; }
        public Theme Theme { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public TypeAccess Access { get; set; }
    }
}
