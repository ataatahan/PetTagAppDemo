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
    public class AlertRepo : GenericRepo<Alert>, IAlertRepo
    {
        public AlertRepo(PetTagAppDbContext context) : base(context) { }

        public ICollection<Alert> GetAlertsByPetId(int petId)
        {
            return _dbSet
                .Where(a => a.PetId == petId)
                .Include(a => a.Pet)
                .ToList();
        }

        public ICollection<Alert> GetAlertsByType(AlertType type)
        {
            return _dbSet
                .Where(a => a.AlertType == type)
                .Include(a => a.Pet)
                .ToList();
        }

        public ICollection<Alert> GetRecentAlerts(int lastDays = 7)
        {
            var threshold = DateTime.Now.AddDays(-lastDays);
            return _dbSet
                .Where(a => a.AlertDate >= threshold)
                .Include(a => a.Pet)
                .ToList();
        }

    }
}
