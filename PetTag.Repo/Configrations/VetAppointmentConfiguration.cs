using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class VetAppointmentConfiguration : IEntityTypeConfiguration<VetAppointment>
    {
        public void Configure(EntityTypeBuilder<VetAppointment> builder)
        {
           
            builder.ToTable("VetAppointments");

            
            builder.HasKey(va => va.Id);

            
            builder.Property(va => va.AppointmentDate)
                   .IsRequired();

            
            builder.Property(va => va.Notes)
                   .HasMaxLength(500);

           
            builder.HasOne(va => va.Vet)
                   .WithMany(v => v.VetAppointments)
                   .HasForeignKey(va => va.VetId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(va => va.Pet)
                   .WithMany(p => p.VetAppointments)
                   .HasForeignKey(va => va.PetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}