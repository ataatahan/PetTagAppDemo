using PetTag.Core.BaseEntities;
using PetTag.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using static PetTag.Core.Exceptions.PetChipExceptions;

namespace PetTag.Core.Entities
{
    public class PetChip : BaseEntity

    {

        public PetChip()
        {
            
        }
        public PetChip(int petId)
        {
            PetId = petId;
            ChipNumber = Guid.NewGuid();
            ChipStatus = ChipStatus.Active;
        }

        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
        public DateTime? DeleteDate { get; set; }
        
        public Guid ChipNumber { get; set; } = Guid.NewGuid();

        public ChipStatus ChipStatus { get; private set; } = ChipStatus.Active;

        public void ChipAsActive()
        { 
            if (ChipStatus == ChipStatus.Pasive)
            {
                ChipStatus = ChipStatus.Active;
                CreateDate = DateTime.Now;
            }
            else
            {
                throw new ChipAlreadyActiveException();
            }
        }

        public void ChipAsPassive()
        {
            if (ChipStatus == ChipStatus.Active)
            {
                ChipStatus = ChipStatus.Pasive;
                CreateDate = DateTime.Now;
            }
            else
            {
                throw new ChipAlreadyPassiveException();
            }
        }

         // --- SON BİLİNEN KONUM ---
        [Column(TypeName = "decimal(9,6)")]
        public decimal? LastLatitude { get; private set; }

        [Column(TypeName = "decimal(9,6)")]
        public decimal? LastLongitude { get; private set; }

        public DateTime? LastLocationAtUtc { get; private set; }

        // Elle konum set etmek için kullanılacak metod
        public void SetLocation(decimal latitude, decimal longitude, DateTime? whenUtc = null)
        {
            if (latitude  < -90  || latitude  > 90)   throw new ArgumentOutOfRangeException(nameof(latitude));
            if (longitude < -180 || longitude > 180)  throw new ArgumentOutOfRangeException(nameof(longitude));

            LastLatitude      = Math.Round(latitude,  6);
            LastLongitude     = Math.Round(longitude, 6);
            LastLocationAtUtc = whenUtc ?? DateTime.UtcNow;
        }

        // burada random konum atanıyor çünkü çipim yok taze bitti çip
        private static readonly Random _rnd = new Random();

        // İstanbul civarı için kaba bir konum(basit ve yeterli)
        // Enlem ~ 40.80–41.25, Boylam ~ 28.50–29.40
        public void SetLocation()
        {
            var lat = RandomBetween(40.80m, 41.25m);
            var lng = RandomBetween(28.50m, 29.40m);
            SetLocation(lat, lng); // yukarıdaki overload'ı çağırır
        }

        private static decimal RandomBetween(decimal min, decimal max)
        {
            var d = (double)min + _rnd.NextDouble() * ((double)max - (double)min);
            return Math.Round((decimal)d, 6);
        }
    }
}
