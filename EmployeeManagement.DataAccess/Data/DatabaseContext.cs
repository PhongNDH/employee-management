using EmployeeManagement.DataAccess.SeedData;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using EmployeeManagement.Models.Entity;

namespace EmployeeManagement.DataAccess.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EthnicGroup> EthnicGroups { get; set; }
        public DbSet<Occupation> Occupations { get; set; }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Commune> Communes { get; set; }

        public DbSet<Diploma> Diplomas { get; set; }    
        public DbSet<AwardDiploma> AwardDiplomas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Province>().HasData(new SeedAdministrativeCountrySubdivision().Provinces);
            modelBuilder.Entity<District>().HasData(new SeedAdministrativeCountrySubdivision().Districts);
            modelBuilder.Entity<Commune>().HasData(new SeedAdministrativeCountrySubdivision().Communes);
            modelBuilder.Entity<Diploma>().HasData(new SeedDiploma().Diplomas);
            
            modelBuilder.Entity<EthnicGroup>().HasData(new SeedEthicGroups().EthicGroups);
            modelBuilder.Entity<Occupation>().HasData(new SeedOccupations().Occupations);
            modelBuilder.Entity<Employee>().HasData(new SeedEmployee().Employees);

        }
    }
}
