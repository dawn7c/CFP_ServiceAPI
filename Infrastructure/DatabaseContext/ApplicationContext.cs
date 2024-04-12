using CfpService.Domain.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DatabaseContext
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Activity>().HasData(
                new Activity { Id = 1, Type = "Report", Description = "Доклад, 35-45 минут" },
                new Activity { Id = 2, Type = "Masterclass", Description = "Мастеркласс, 1-2 часа" },
                new Activity { Id = 3, Type = "Discussion", Description = "Дискуссия / круглый стол, 40-50 минут" }
            );
        }
    }
}
