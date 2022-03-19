using ALTIELTS.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALTIELTS.DatabaseContext
{
    public class StartCampContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public StartCampContext(DbContextOptions<StartCampContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<Entities.Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                            .ToTable("User")
                            .HasKey(x=>x.Id);

            modelBuilder.Entity<SurveyQuestion>()
                            .ToTable("SurveyQuestion")
                            .HasKey(x => x.Id);

            modelBuilder.Entity<Entities.Service>().ToTable("Service").HasKey(x => x.Id);
            modelBuilder.Entity<Entities.Service>().HasMany(x => x.SurveyQuestion).WithOne(x => x.Service).HasForeignKey(x => x.ServiceId);
                            
            modelBuilder.Entity<RatingResult>().ToTable("RatingResult").HasKey(x => x.Id);
            modelBuilder.Entity<RatingResult>().HasOne(x => x.SurveyQuestion).WithMany(x => x.RatingResults).HasForeignKey(x => x.QuestionId);
            modelBuilder.Entity<RatingResult>().HasOne(x => x.User).WithMany(x => x.RatingResults).HasForeignKey(x => x.UserId);

        }
    }
}
