using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.DataAccess.Config
{
    internal class AwardConfig : IEntityTypeConfiguration<AwardDiploma>
    {
        public void Configure(EntityTypeBuilder<AwardDiploma> builder)
        {
            builder.HasOne(b => b.DiplomaGrantingUnit).WithMany().HasForeignKey(p => p.DiplomaGrantingUnitId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(b => b.Diploma).WithMany().HasForeignKey(p => p.DiplomaId);
            builder.HasOne(b => b.Employee).WithMany().HasForeignKey(p => p.EmployeeId);
            builder.Property(b => b.GrantingDate).HasDefaultValue(DateTime.Now);
        }
    }
}
