namespace PetTag.Core.Exceptions
{
    public class PetOwnerException : Exception
    {
        public class InvalidPetOwnerEmailException : Exception
        {
            public InvalidPetOwnerEmailException() 
                : base("Pet sahibi e-posta adresi boş olamaz ve 50 karakterden uzun olamaz.") { }
        }

        public class InvalidPetOwnerFirstNameException : Exception
        {
            public InvalidPetOwnerFirstNameException()
                : base("Pet sahibi adı boş olamaz ve 50 karakterden uzun olamaz.") { }
        }

        public class InvalidPetOwnerLastNameException : Exception
        {
            public InvalidPetOwnerLastNameException()
                : base("Pet sahibi soyadı boş olamaz ve 50 karakterden uzun olamaz.") { }
        }

    }
}
