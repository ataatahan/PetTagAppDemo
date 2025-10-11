using PetTag.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetTag.Core.Exceptions.VetExceptions;

namespace PetTag.Core.Entities
{

    public class Vet : BaseEntity
    {
        private string _firstName;
        private string _phoneNumber;
        private string _lastName;
        public Vet()
        {
            
        }
        public Vet (string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (value.Length > 50 || string.IsNullOrWhiteSpace(value))
                    throw new InvalidVetFirstNameException();
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Length > 50 || string.IsNullOrWhiteSpace(value))
                    throw new InvalidVetLastNameException();
                _lastName = value;
            }
        }
        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && value.Length == 11)
                    _phoneNumber = value;
                else
                    throw new InvalidVetPhoneNumberException();
            }
        }
        public string FullName => _firstName + " " + _lastName;

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
        public ICollection<VetAppointment> VetAppointments { get; set; } = new List<VetAppointment>();

    }
}

