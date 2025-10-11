using PetTag.Core.Entities;
using PetTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IAlertRepo : IGenericRepo<Alert>
    {
        ICollection<Alert> GetAlertsByPetId(int petId);
        ICollection<Alert> GetAlertsByType(AlertType type);
        ICollection<Alert> GetRecentAlerts(int lastDays = 7);
    }
}
