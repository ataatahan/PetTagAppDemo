using PetTag.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IPetOwnerRepo : IGenericRepo<PetOwner>
    {
        PetOwner? GetOwnerByEmail(string email);
        ICollection<PetOwner> GetOwnersWithPets();
        PetOwner? GetOwnerWithPetsById(int ownerId);
    }

}
