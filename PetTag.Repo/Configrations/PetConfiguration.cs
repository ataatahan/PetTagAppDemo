using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            
            builder.ToTable("Pets");

           
            builder.HasKey(p => p.Id);

            
            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            
            builder.Property(p => p.Age)
                   .IsRequired();

          
            builder.Property(p => p.Weight)
                   .IsRequired()
                   .HasColumnType("decimal(5,2)");

            // enum
            builder.Property(p => p.Type)
                   .HasConversion<int>()
                   .IsRequired();

            // PetChip ilişkisi (1-1)
            builder.HasOne(p => p.PetChip)
                   .WithOne(pc => pc.Pet)
                   .HasForeignKey<PetChip>(pc => pc.PetId);

            // PetOwner ilişkisi (n-1)
            builder.HasOne(p => p.PetOwner)
                   .WithMany(po => po.Pets)
                   .HasForeignKey(p => p.PetOwnerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Alerts ilişkisi (1-n)
            builder.HasMany(p => p.Alerts)
                   .WithOne(a => a.Pet)
                   .HasForeignKey(a => a.PetId);

            // HealthRecords ilişkisi (1-n)
            builder.HasMany(p => p.HealtRecords)
                   .WithOne(hr => hr.Pet)
                   .HasForeignKey(hr => hr.PetId);

            // ActivityLogs ilişkisi (1-n)
            builder.HasMany(p => p.ActivityLogs)
                   .WithOne(al => al.Pet)
                   .HasForeignKey(al => al.PetId);

            // VetAppointments ilişkisi (1-n)
            builder.HasMany(p => p.VetAppointments)
                   .WithOne(va => va.Pet)
                   .HasForeignKey(va => va.PetId);
        }
    }
}
