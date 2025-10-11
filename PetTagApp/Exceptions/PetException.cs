using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Core.Exceptions
{
    public class PetException
    {
        public class InvalidPetNameException : Exception
        {
            public InvalidPetNameException()
                : base("Pet adı boş olamaz veya 50 karakterden uzun olamaz.") { }
        }

        public class InvalidPetAgeException : Exception
        {
            public InvalidPetAgeException()
                : base("Pet yaşı 0 ile 150 arasında olmalıdır.") { }
        }

        public class InvalidPetWeightException : Exception
        {
            public InvalidPetWeightException()
                : base("Pet kilosu 0'dan küçük veya eşit olamaz.") { }
        }

    }
}
