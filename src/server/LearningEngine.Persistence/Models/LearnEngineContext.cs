using LearningEngine.Persistence.Utils;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LearningEngine.Persistence.Models
{
    public class LearnEngineContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }

        public LearnEngineContext(DbContextOptions<LearnEngineContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(prm => prm.User)
                .WithMany(usr => usr.Permissions)
                .HasForeignKey(prm => prm.UserId);
            modelBuilder.Entity<Permission>()
                .HasOne(prm => prm.Theme)
                .WithMany(thm => thm.Permissions)
                .HasForeignKey(prm => prm.ThemeId);

            modelBuilder.Entity<User>()
                .HasIndex(usr => usr.UserName)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(usr => usr.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .Property(usr => usr.Password)
                .IsRequired()
                .HasMaxLength(64);
            modelBuilder.Entity<User>()
                .Property(usr => usr.Email)
                .HasMaxLength(100);
            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = 1,
                    Email = "rolit@mail.cor",
                    UserName = "rolit",
                    Password = PasswordHasher.GetHash("123", "rolit")
                });

            modelBuilder.Entity<Theme>()
                .HasKey(t => t.Id);
            modelBuilder.Entity<Theme>()
                .HasOne(thm => thm.ParentTheme)
                .WithMany(thm => thm.SubThemes)
                .HasForeignKey(thm => thm.ParentThemeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Theme>()
                .Property(thm => thm.Name)
                .IsRequired();
            modelBuilder.Entity<Theme>()
                .Property(thm => thm.Description)
                .IsRequired();

            modelBuilder.Entity<Theme>()
                .HasData(new List<Theme> {
                    new Theme
                    {
                        Id = 2,
                        Name = "dotNet",
                        Description = "all about .NET",
                        IsPublic = true,
                    },
                    new Theme
                    {
                        Id = 3,
                        Name = "linq",
                        Description = "all about linq",
                        IsPublic = true,
                        ParentThemeId = 2
                    }
                });

            modelBuilder.Entity<Note>()
                .HasData(new List<Note>
                {
                    new Note
                    {
                        Id = 1,
                        ThemeId = 3,
                        Content = "deffered execution exist",
                        Title = "deffered execution"
                    },
                    new Note
                    {
                        Id = 2,
                        ThemeId = 3,
                        Content = "GC - is garbage collector",
                        Title = "GC "
                    }
                });
        }
    }
}
