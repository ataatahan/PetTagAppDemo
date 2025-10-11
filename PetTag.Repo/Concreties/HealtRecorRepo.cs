using Microsoft.EntityFrameworkCore;
using PetTag.Core.Entities;
using PetTag.Repo.Contexts;
using PetTag.Repo.Interfaces;
using System;

namespace PetTag.Repo.Concretes
{
    public class HealthRecordRepo : GenericRepo<HealtRecord>, IHealthRecordRepo
    {
        public HealthRecordRepo(PetTagAppDbContext context) : base(context) { }

        public ICollection<HealtRecord> GetRecordsByPetId(int petId)
        {
            return _dbSet
                .Where(hr => hr.PetId == petId)
                .Include(hr => hr.Pet)
                .ToList();
        }

        public ICollection<HealtRecord> GetVaccinationRecords()
        {
            return _dbSet
                .Where(hr => hr.IsVaccination)
                .Include(hr => hr.Pet)
                .ToList();
        }

        public ICollection<HealtRecord> GetRecordsByDateRange(DateTime start, DateTime end)
        {
            return _dbSet
                .Where(hr => hr.RecordDate >= start && hr.RecordDate <= end)
                .Include(hr => hr.Pet)
                .ToList();
        }
    }
}
