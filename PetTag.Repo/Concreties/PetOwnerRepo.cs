using Microsoft.EntityFrameworkCore;
using PetTag.Core.Entities;
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
    public class PetOwnerRepo : GenericRepo<PetOwner>, IPetOwnerRepo
    {
        public PetOwnerRepo(PetTagAppDbContext context) : base(context) { }

        public PetOwner? GetOwnerByEmail(string email)
        {
            return _dbSet
                .FirstOrDefault(po => po.Email.ToLower() == email.ToLower());
        }

        public ICollection<PetOwner> GetOwnersWithPets()
        {
            return _dbSet
                .Include(po => po.Pets)
                .ToList();
        }

        public PetOwner? GetOwnerWithPetsById(int ownerId)
        {
            return _dbSet
                .Include(po => po.Pets)
                .FirstOrDefault(po => po.Id == ownerId);
        }
    }

}
