using PetTag.Core.BaseEntities;
using static PetTag.Core.Exceptions.PetOwnerException;

namespace PetTag.Core.Entities
{
    public class PetOwner : BaseEntity
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        public PetOwner()
        {
            
        }

        public PetOwner(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName
        {
            get => _firstName;
            set 
            {
                if (value.Length> 50 || string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidPetOwnerFirstNameException();
                }
                else 
                    _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length > 50 || string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidPetOwnerLastNameException();
                }
                else
                    _lastName = value;
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                if (value.Length > 50 || string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidPetOwnerEmailException();
                }
                else
                    _email = value;
            }
        }

        public string FullName => _firstName + " " + _lastName;

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
