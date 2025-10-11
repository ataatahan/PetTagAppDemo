using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Core.Exceptions
{
    public class VetExceptions
    {
        public class InvalidVetPhoneNumberException : Exception
        {
            public InvalidVetPhoneNumberException()
                : base("Veteriner telefon numarası 11 haneli olmalıdır.") { }
        }

        public class InvalidVetFirstNameException : Exception
        {
            public InvalidVetFirstNameException()
                : base("Veteriner adı boş olamaz ve 50 karakterden uzun olamaz.") { }
        }

        public class InvalidVetLastNameException : Exception
        {
            public InvalidVetLastNameException()
                : base("Veteriner soyadı boş olamaz ve 50 karakterden uzun olamaz.") { }
        }
    }
}
