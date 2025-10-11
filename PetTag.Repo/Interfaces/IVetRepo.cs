using PetTag.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IVetRepo : IGenericRepo<Vet>
    {
        Vet? GetVetByFullName(string fullName);
        ICollection<Vet> GetVetsWithAppointments();
        ICollection<Vet> GetVetsWithPets();
    }
}
