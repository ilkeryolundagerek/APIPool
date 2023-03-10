using API01.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API01.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Active).IsRequired().HasDefaultValue(true);
            builder.Property(x => x.Deleted).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.CreateTime).IsRequired().HasDefaultValueSql("getdate()");
            builder.Property(x => x.UpdateTime).IsRequired(false);
            builder.Property(x => x.DeleteTime).IsRequired(false);

            builder.Property(x => x.Title).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(500);
            builder.ToTable("Department", "HR");
        }
    }
}
