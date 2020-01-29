using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningEngine.Persistence.Models
{
    public class LearnEngineContext: DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LearnEngineDb;Trusted_Connection=true");
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

            modelBuilder.Entity<Theme>()
                .HasOne(thm => thm.ParentTheme)
                .WithMany(thm => thm.SubThemes)
                .HasForeignKey(thm => thm.ParentThemeId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
