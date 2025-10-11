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
    public class VetAppointmentRepo : GenericRepo<VetAppointment>, IVetAppointmentRepo
    {
        public VetAppointmentRepo(PetTagAppDbContext context) : base(context) { }

        public ICollection<VetAppointment> GetAppointmentsByPetId(int petId)
        {
            return _dbSet
                .Where(a => a.PetId == petId)
                .Include(a => a.Pet)
                .Include(a => a.Vet)
                .ToList();
        }

        public ICollection<VetAppointment> GetAppointmentsByVetId(int vetId)
        {
            return _dbSet
                .Where(a => a.VetId == vetId)
                .Include(a => a.Vet)
                .Include(a => a.Pet)
                .ToList();
        }

        public ICollection<VetAppointment> GetAppointmentsByDateRange(DateTime start, DateTime end)
        {
            return _dbSet
                .Where(a => a.AppointmentDate >= start && a.AppointmentDate <= end)
                .Include(a => a.Vet)
                .Include(a => a.Pet)
                .ToList();
        }
    }

}
