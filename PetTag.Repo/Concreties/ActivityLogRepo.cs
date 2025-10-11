using Microsoft.EntityFrameworkCore;
using PetTag.Core.Entities;
using PetTag.Repo.Contexts;
using PetTag.Repo.Interfaces;
using System;

namespace PetTag.Repo.Concretes
{
    public class ActivityLogRepo : GenericRepo<ActivityLog>, IActivityLogRepo
    {
        public ActivityLogRepo(PetTagAppDbContext context) : base(context) { }

        public ICollection<ActivityLog> GetLogsByPetId(int petId)
        {
            return _dbSet
                .Where(log => log.PetId == petId)
                .Include(log => log.Pet)
                .ToList();
        }

        public ICollection<ActivityLog> GetLogsByDateRange(DateTime start, DateTime end)
        {
            return _dbSet
                .Where(log => log.LogDate >= start && log.LogDate <= end)
                .Include(log => log.Pet)
                .ToList();
        }
    }
}
