using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using MiniSurveys.Domain.Data.ModelConfigurations;
using MiniSurveys.Domain.Modals;

namespace MiniSurveys.Domain.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IDataProtectionKeyContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

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
