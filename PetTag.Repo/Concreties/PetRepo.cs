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
    public class PetRepo : GenericRepo<Pet>, IPetRepo
    {
        public PetRepo(PetTagAppDbContext context) : base(context) { }

        public ICollection<Pet> GetPetsWithChip()
        {
            return _dbSet.Include(p => p.PetChip).ToList();
        }

        public ICollection<Pet> GetPetsWithOwner()
        {
            return _dbSet.Include(p => p.PetOwner).ToList();
        }

        public ICollection<Pet> GetPetsWithVet()
        {
            return _dbSet.Include(p => p.Vet).ToList();
        }

        public ICollection<Pet> GetPetsWithAllDetails()
        {
            return _dbSet
                .Include(p => p.PetChip)
                .Include(p => p.PetOwner)
                .Include(p => p.Vet)
                .Include(p => p.VetAppointments)
                .Include(p => p.Alerts)
                .Include(p => p.HealtRecords)
                .Include(p => p.ActivityLogs)
                .ToList();
        }

    }
}
