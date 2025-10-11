using PetTag.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IPetRepo : IGenericRepo<Pet>
    {
        ICollection<Pet> GetPetsWithChip();
        ICollection<Pet> GetPetsWithOwner();
        ICollection<Pet> GetPetsWithVet();
        ICollection<Pet> GetPetsWithAllDetails();

    }
}
