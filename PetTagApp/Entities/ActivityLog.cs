using PetTag.Core.BaseEntities;
using static PetTag.Core.Exceptions.ActivityLogExceptions;

namespace PetTag.Core.Entities
{
    public class ActivityLog : BaseEntity
    {
        // bildiğiniz gibi ef core için parametresiz bir ctor şart o yüzden eklendi 
        public ActivityLog() { }

        
        public ActivityLog(int petId, double? walkingMinutes, double? runningMinutes, double? sleepingMinutes, double? temperature, double? distance)
        {
            Validate(walkingMinutes, runningMinutes, sleepingMinutes, temperature, distance);

            PetId = petId;
            WalkingMinutes = walkingMinutes;
            RunningMinutes = runningMinutes;
            SleepingMinutes = sleepingMinutes;
            Temperature = temperature;
            Distance = distance;
            LogDate = DateTime.Now;
        }

        public DateTime LogDate { get; set; } = DateTime.Now;

        // Ef core migration için public get,set bırakıyoruz
        public double? WalkingMinutes { get; set; }
        public double? RunningMinutes { get; set; }
        public double? SleepingMinutes { get; set; }
        public double? Temperature { get; set; }
        public double? Distance { get; set; }

        // Navigation proportyler de burada hocam
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        // validation yapıyoruz ya dedik ki ayrı metoda yapalım daha hoş olur hepsine null geçilebilir dedik ki ihtiyaca göre kullanalım
        private void Validate(double? walk, double? run, double? sleep, double? temp, double? dist)
        {
            if (walk < 0) throw new NegativeWalkingMinutesException();
            if (run < 0) throw new NegativeRunningMinutesException();
            if (sleep > 48) throw new InvalidSleepingMinutesException48();
            if (sleep < 0 || sleep > 24) throw new InvalidSleepingMinutesException();
            if (temp < 0) throw new InvalidTemperatureException();
            if (dist < 0 || dist > 1000) throw new InvalidDistanceException();
        }
    }
}
