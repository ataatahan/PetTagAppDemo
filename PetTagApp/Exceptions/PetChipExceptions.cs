namespace PetTag.Core.Exceptions
{
    public class PetChipExceptions : Exception
    {
        public class ChipAlreadyActiveException : Exception
        {
            public ChipAlreadyActiveException()
                : base("Bu çip zaten aktif durumda.") { }
        }

        public class ChipAlreadyPassiveException : Exception
        {
            public ChipAlreadyPassiveException()
                : base("Bu çip zaten pasif durumda.") { }
        }

        public class InvalidChipLocationException : Exception
        {
            public InvalidChipLocationException(string message)
                : base(message) { }
        }
    }
}
