 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class PetChipConfiguration : IEntityTypeConfiguration<PetChip>
    {
        public void Configure(EntityTypeBuilder<PetChip> builder)
        {
           
            builder.ToTable("PetChips");

           
            builder.HasKey(pc => pc.Id);

           
            builder.Property(pc => pc.ChipNumber)
                   .IsRequired();

            builder.HasIndex(pc => pc.ChipNumber)
                   .IsUnique();

            
            builder.Property(pc => pc.CreateDate)
                   .HasDefaultValueSql("GETDATE()");

            
            builder.Property(pc => pc.Status)
                   .HasConversion<int>()
                   .IsRequired();

            
            builder.Property(pc => pc.LastLatitude)
                   .HasColumnType("decimal(9,6)");

            builder.Property(pc => pc.LastLongitude)
                   .HasColumnType("decimal(9,6)");

            
            builder.Property(pc => pc.LastLocationAtUtc)
                   .IsRequired(false);

            
            builder.HasOne(pc => pc.Pet)
                   .WithOne(p => p.PetChip)
                   .HasForeignKey<PetChip>(pc => pc.PetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
