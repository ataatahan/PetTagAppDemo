using Microsoft.EntityFrameworkCore;
using PetTag.Core.Entities;
using PetTag.Core.Enums;
using PetTag.Repo.Concretes;
using PetTag.Repo.Contexts;
using PetTag.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Concreties
{
    public class PetChipRepo : GenericRepo<PetChip>, IPetChipRepo
    {
        public PetChipRepo(PetTagAppDbContext context) : base(context) { }

        public PetChip? GetChipByPetId(int petId)
        {
            return _dbSet
                .Include(pc => pc.Pet)
                .FirstOrDefault(pc => pc.PetId == petId);
        }

        public PetChip? GetChipByNumber(Guid chipNumber)
        {
            return _dbSet
                .Include(pc => pc.Pet)
                .FirstOrDefault(pc => pc.ChipNumber == chipNumber);
        }

        public ICollection<PetChip> GetActiveChips()
        {
            return _dbSet
                .Where(pc => pc.ChipStatus == ChipStatus.Active)
                .Include(pc => pc.Pet)
                .ToList();
        }

        public ICollection<PetChip> GetPassiveChips()
        {
            return _dbSet
                .Where(pc => pc.ChipStatus == ChipStatus.Pasive)
                .Include(pc => pc.Pet)
                .ToList();
        }
    }

}
