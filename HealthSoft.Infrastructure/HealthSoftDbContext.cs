using HealthSoft.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthSoft.Infrastructure
{
    public class HealthSoftDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        public HealthSoftDbContext(DbContextOptions<HealthSoftDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new DoctorEntityConfigurations().Configure(modelBuilder.Entity<Doctor>());
            new PatientEntityConfigurations().Configure(modelBuilder.Entity<Patient>());
            new AppointmentEntityConfigurations().Configure(modelBuilder.Entity<Appointment>());
            new AppUserEntityConfigurations().Configure(modelBuilder.Entity<AppUser>());
        }
    }

    internal class DoctorEntityConfigurations : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasQueryFilter(doc => !doc.IsDeleted);
            builder.HasIndex(doc => doc.IsDeleted)
                .HasFilter("IsDeleted=0");
        }
    }
    internal class PatientEntityConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasQueryFilter(pat => !pat.IsDeleted);
            builder.HasIndex(pat => pat.IsDeleted)
                .HasFilter("IsDeleted=0");
        }
    }
    internal class AppointmentEntityConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasQueryFilter(apt => !apt.IsDeleted);
            builder.HasIndex(apt => apt.IsDeleted)
                .HasFilter("IsDeleted=0");
        }
    }
    internal class AppUserEntityConfigurations : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasQueryFilter(usr => !usr.IsDeleted);
            builder.HasIndex(usr => usr.IsDeleted)
                .HasFilter("IsDeleted=0");
        }
    }
}