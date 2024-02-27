using EmployeeManagement.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagement.DataAccess.Config
{
    internal class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasOne(b => b.Province).WithMany().HasForeignKey(p => p.ProvinceId);
        }
    }
}
