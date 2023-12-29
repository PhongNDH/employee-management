using EmployeeManagement.Models;
using EmployeeManagement.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace EmployeeManagement.DataAccess.Config
{
    internal class CommuneConfig : IEntityTypeConfiguration<Commune>
    {
        public void Configure(EntityTypeBuilder<Commune> builder)
        {
            builder.HasOne(b => b.District).WithMany().HasForeignKey(p => p.DistrictId);
        }
    }
}
