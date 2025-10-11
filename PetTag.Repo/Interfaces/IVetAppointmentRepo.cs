using PetTag.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IVetAppointmentRepo : IGenericRepo<VetAppointment>
    {
        ICollection<VetAppointment> GetAppointmentsByPetId(int petId);
        ICollection<VetAppointment> GetAppointmentsByVetId(int vetId);
        ICollection<VetAppointment> GetAppointmentsByDateRange(DateTime start, DateTime end);
    }

}
