using example.domain.Entities;
using example.infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace example.infrastructure
{
    public class ExampleDbContext : DbContext
    {
        public ExampleDbContext(DbContextOptions<ExampleDbContext> options) : base(options)
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.CreateDepartmentBuilder();
            modelBuilder.CreateUserBuilder();
            modelBuilder.CreateSalaryBuilder();
        }

        /* Migration DB CLI
         * Step 1: add-migration InitialExampleDB
         * Step 2: dotnet ef migrations add InitialExampleDB
         * Step 3: update-database -verbose
         * Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

         */
    }
}
