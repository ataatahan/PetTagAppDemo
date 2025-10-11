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
    public class VetRepo : GenericRepo<Vet>, IVetRepo
    {
        public VetRepo(PetTagAppDbContext context) : base(context) { }

        public Vet? GetVetByFullName(string fullName)
        {
            return _dbSet
                .FirstOrDefault(v => (v.FirstName + " " + v.LastName).ToLower() == fullName.ToLower());
        }

        public ICollection<Vet> GetVetsWithAppointments()
        {
            return _dbSet
                .Include(v => v.VetAppointments)
                .ToList();
        }

        public ICollection<Vet> GetVetsWithPets()
        {
            return _dbSet
                .Include(v => v.Pets)
                .ToList();
        }
    }

}
