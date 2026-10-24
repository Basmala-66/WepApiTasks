using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Health_Care_System.Data.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e=>new {e.PatientId,e.DoctorId});

            builder.HasIndex(e => e.DoctorId, "IX_Appointments_DoctorId");

            builder.HasOne(d => d.Doctor).WithMany(p => p.Appointments).HasForeignKey(d => d.DoctorId);

            builder.HasOne(d => d.Patient).WithMany(p => p.Appointments).HasForeignKey(d => d.PatientId);


            builder.HasQueryFilter(p => !p.IsDeleted);


        }
    }
}
