using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class VetConfiguration : IEntityTypeConfiguration<Vet>
    {
        public void Configure(EntityTypeBuilder<Vet> builder)
        {
            // Tablo adı
            builder.ToTable("Vets");

            // Primary Key
            builder.HasKey(v => v.Id);

            // FirstName zorunlu ve max uzunluk
            builder.Property(v => v.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            // LastName zorunlu ve max uzunluk
            builder.Property(v => v.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            // PhoneNumber max uzunluk verdik
            builder.Property(v => v.PhoneNumber)
                   .HasMaxLength(20);

            

            // Pet ilişkisi (1-n)
            builder.HasMany(v => v.Pets)
                   .WithOne()
                   .HasForeignKey(p => p.PetOwnerId) 
                   .OnDelete(DeleteBehavior.NoAction); 
        }
    }
}
