using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class AlertConfiguration : IEntityTypeConfiguration<Alert>
    {
        public void Configure(EntityTypeBuilder<Alert> builder)
        {
            
            builder.ToTable("Alerts");

           
            builder.HasKey(a => a.Id);

            // AlertType ın enum olarak saklanmasını sağladık int ile 
            builder.Property(a => a.AlertType)
                   .HasConversion<int>() // Enum'ı int olarak sakla
                   .IsRequired(false);

            // max uzunluk verdik mesaja yormasın datayı
            builder.Property(a => a.Message)
                   .HasMaxLength(250);

            // AlertDate default olarak şimdiki zaman dedik oluşunca atanacak
            builder.Property(a => a.AlertDate)
                   .HasDefaultValueSql("GETDATE()");

            // PetId zorunlu kıldık
            builder.Property(a => a.PetId)
                   .IsRequired();

            //  (1-n) ilişki verdik
            builder.HasOne(a => a.Pet)
                   .WithMany(p => p.Alerts)
                   .HasForeignKey(a => a.PetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
