using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health_Care_System.Data.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(p => p.Name)
             .IsRequired()
             .HasMaxLength(50);



        }
    }
}
