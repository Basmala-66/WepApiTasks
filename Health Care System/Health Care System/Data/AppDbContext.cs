using System;
using System.Collections.Generic;
using Health_Care_System.Entities;
using Microsoft.EntityFrameworkCore;

namespace Health_Care_System.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database= HealthCareSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => new { e.PatientId, e.DoctorId });

            entity.HasIndex(e => e.DoctorId, "IX_Appointments_DoctorId");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments).HasForeignKey(d => d.DoctorId);

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments).HasForeignKey(d => d.PatientId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
