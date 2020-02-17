using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<Permission> Permissions { get; set; }
    }
}
