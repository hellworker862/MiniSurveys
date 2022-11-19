using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniSurveys.Domain.Data.ModelConfigurations;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Кадровая служба" },
                new Department { Id = 2, Name = "Отдел разработки", });
            base.OnModelCreating(modelBuilder);
        }
    }
}
