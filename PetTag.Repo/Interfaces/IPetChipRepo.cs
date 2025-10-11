using PetTag.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IPetChipRepo : IGenericRepo<PetChip>
    {
        PetChip? GetChipByPetId(int petId);
        PetChip? GetChipByNumber(Guid chipNumber);
        ICollection<PetChip> GetActiveChips();
        ICollection<PetChip> GetPassiveChips();
    }

}
