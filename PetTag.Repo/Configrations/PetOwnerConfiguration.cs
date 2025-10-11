using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class PetOwnerConfiguration : IEntityTypeConfiguration<PetOwner>
    {
        public void Configure(EntityTypeBuilder<PetOwner> builder)
        {
           
            builder.ToTable("PetOwners");

            
            builder.HasKey(po => po.Id);

            
            builder.Property(po => po.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

          
            builder.Property(po => po.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            
            builder.Property(po => po.Email)
                   .IsRequired()
                   .HasMaxLength(50);

         

            // Pet ilişkisi (1-n)
            builder.HasMany(po => po.Pets)
                   .WithOne(p => p.PetOwner)
                   .HasForeignKey(p => p.PetOwnerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
