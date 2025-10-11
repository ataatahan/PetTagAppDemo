using PetTag.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Core.Entities
{
    public class VetAppointment : BaseEntity
    {
        public DateTime AppointmentDate { get; set; }

        public int VetId { get; set; }
        public Vet Vet { get; set; }

        public string? Notes { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
