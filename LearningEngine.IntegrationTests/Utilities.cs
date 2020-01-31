using LearningEngine.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LearningEngine.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(LearnEngineContext db)
        {
            db.Users.AddRange(GetUser());
            db.Themes.AddRange(GetThemes());
            db.SaveChanges();
            db.Permissions.AddRange(GetPermission(db));
            db.SaveChanges();
        }

        

        public static void ReinitializeDbForTests(LearnEngineContext db)
        {
            db.Users.RemoveRange(db.Users);
            InitializeDbForTests(db);
        }


        private static List<Theme> GetThemes()
        {
            return new List<Theme>
            {
                new Theme
                {
                    Name = ".net",
                    Description = "some .net theme",
                    IsPublic = true
                },
                new Theme
                {
                    Name = "cp",
                    Description = "central processor",
                    IsPublic = false
                }
            };
        }

        public static List<User> GetUser()
        {
            return new List<User>
            {
                new User
                {
                    UserName = "alex",
                    Email = "alex@mail.ru",
                    Password = "1234"
                },
                new User
                {
                    UserName = "vitas",
                    Email = "lala@mail.com",
                    Password = "123"
                },
                new User
                {
                    UserName = "micJac",
                    Email = "gladkiyKriminal@yahoo.kz",
                    Password = "123"
                }
            };
        }

        private static List<Permission> GetPermission(LearnEngineContext db)
        {
            var users = db.Users.ToList();
            var theme = db.Themes.ToList();
            return new List<Permission>{
                new Permission
                {
                    UserId = users[0].Id,
                    ThemeId = theme[0].Id,
                    Access = TypeAccess.Read | TypeAccess.Write
                },
                new Permission
                {
                    UserId = users[1].Id,
                    ThemeId = theme[1].Id,
                    Access = TypeAccess.Read | TypeAccess.Write
                },
                new Permission
                {
                    UserId = users[0].Id,
                    ThemeId = theme[1].Id,
                    Access = TypeAccess.Read
                }
            };
        }
    }
}
