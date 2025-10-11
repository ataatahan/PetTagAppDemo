using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetTag.Core.Entities;

namespace PetTag.Repo.Configurations
{
    public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
    {
        public void Configure(EntityTypeBuilder<ActivityLog> builder)
        {
            // burada tablo adını belirledik databasedeki
            builder.ToTable("ActivityLogs");

            // primarykey olduğunu söyledik
            builder.HasKey(a => a.Id);

            // petId yi boş geçilemez yaptık
            builder.Property(a => a.PetId)
                   .IsRequired();

            // LogDate e defoult bir değeer girdik
            builder.Property(a => a.LogDate)
                   .HasDefaultValueSql("GETDATE()");

            // yürüme zamanının database de nasıl ne türde tutulacağını yazdık 
            builder.Property(a => a.WalkingMinutes)
                   .HasPrecision(5, 2);

            // koşma zamanının database de nasıl ne türde tutulacağını yazdık 
            builder.Property(a => a.RunningMinutes)
                   .HasPrecision(5, 2);

            // uyuma zamanının database de nasıl ne türde tutulacağını yazdık 
            builder.Property(a => a.SleepingMinutes)
                   .HasPrecision(5, 2);

            // sıcaklığın database de nasıl ne türde tutulacağını yazdık 
            builder.Property(a => a.Temperature)
                   .HasPrecision(5, 2);

           
            builder.Property(a => a.Distance)
                   .HasPrecision(8, 2);

            // İlişki 1 e n ilişki verdik
            builder.HasOne(a => a.Pet)
                   .WithMany(p => p.ActivityLogs)
                   .HasForeignKey(a => a.PetId)
                   .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
