using Microsoft.EntityFrameworkCore;
using PetTag.Core.Entities;
using PetTag.Repo.Configurations;
using System.Diagnostics;

namespace PetTag.Repo.Contexts
{
    public class PetTagAppDbContext : DbContext
    {
        public PetTagAppDbContext()
        {
        }

        public PetTagAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }
        public DbSet<HealtRecord> HealtRecords { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetChip> PetChips { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<VetAppointment> VetAppointments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-NCN7N8Q;Initial Catalog=PetTagAppDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VetAppointmentConfiguration());
            modelBuilder.ApplyConfiguration(new VetConfiguration());
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new PetOwnerConfiguration());
            modelBuilder.ApplyConfiguration(new PetChipConfiguration());
            modelBuilder.ApplyConfiguration(new AlertConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityLogConfiguration());
            modelBuilder.ApplyConfiguration(new HealthRecordConfiguration());
        }
    }
}
