namespace PetTag.Core.Exceptions
{
    public class ActivityLogExceptions
    {
        public class NegativeWalkingMinutesException : Exception
        {
            public NegativeWalkingMinutesException()
                : base("Yürüme süresi 0'dan küçük olamaz.") { }
        }

        public class NegativeRunningMinutesException : Exception
        {
            public NegativeRunningMinutesException()
                : base("Koşma süresi 0'dan küçük olamaz.") { }
        }

        public class InvalidSleepingMinutesException : Exception
        {
            public InvalidSleepingMinutesException()
                : base("Uyuma süresi 0-24 saat arasında olmalı.") { }
        }

        public class InvalidSleepingMinutesException48 : Exception
        {
            public InvalidSleepingMinutesException48()
                : base("48 saatten fazla ise hayvan ex olmuş olabilir.") { }
        }

        public class InvalidTemperatureException : Exception
        {
            public InvalidTemperatureException()
                : base("Sıcaklık değeri 0'dan küçük olamaz.") { }
        }

        public class InvalidDistanceException : Exception
        {
            public InvalidDistanceException()
                : base("Mesafe 0'dan küçük veya 1000'den büyük olamaz.") { }
        }

    }
}
