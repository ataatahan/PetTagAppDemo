using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class HealthRecordConfiguration : IEntityTypeConfiguration<HealtRecord>
    {
        public void Configure(EntityTypeBuilder<HealtRecord> builder)
        {
            
            builder.ToTable("HealthRecords");

            
            builder.HasKey(h => h.Id);

            
            builder.Property(h => h.Description)
                   .IsRequired()
                   .HasMaxLength(500);

           
            builder.Property(h => h.RecordDate)
                   .IsRequired();

            
            builder.Property(h => h.IsVaccination)
                   .IsRequired();

            
            builder.Property(h => h.Treatment)
                   .HasMaxLength(300);

            
            builder.Property(h => h.PetId)
                   .IsRequired();

            
            builder.HasOne(h => h.Pet)
                   .WithMany(p => p.HealtRecords)
                   .HasForeignKey(h => h.PetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
