using PetTag.Core.BaseEntities;
using PetTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PetTag.Core.Exceptions.PetException;

namespace PetTag.Core.Entities
{
    public class Pet : BaseEntity
    {
        public Pet()
        {
            
        }
        public Pet( string name , int age)
        {
              Name = name;
              Age = age;
        }

        public PetType Type { get; set; }
        private int _age;
        private double _weight;
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (value.Length > 50 || string.IsNullOrEmpty(value))
                    throw new InvalidPetNameException();
                else
                    _name = value;
            }
        }
        public int Age
        {
            get => _age;
            set
            {
                if (value < 0 || value > 150)
                    throw new InvalidPetAgeException();
                else
                    _age = value;
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                if (value <= 0)
                    throw new InvalidPetWeightException();
                else
                    _weight = value;
            }
        }

        //PetChip
        
        public virtual PetChip PetChip { get; set; }

        //PetOwner
        public int PetOwnerId { get; set; }
        public PetOwner PetOwner { get; set; }

        //Vet
        public int VetId { get; set; }
        public Vet Vet { get; set; }


        public ICollection<VetAppointment> VetAppointments { get; set; } = new List<VetAppointment>();

        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();

        public ICollection<HealtRecord> HealtRecords { get; set; } = new List<HealtRecord>();

        public ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();
    }
}
