using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.DataAccess.Config
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.CommuneId).IsRequired(false);
            builder.HasOne(b => b.EthnicGroup).WithMany().HasForeignKey(p => p.EthnicGroupId);
            builder.HasOne(b => b.Occupation).WithMany().HasForeignKey(p => p.OccupationId);
            builder.HasOne(b => b.Commune).WithMany().HasForeignKey(p => p.CommuneId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
