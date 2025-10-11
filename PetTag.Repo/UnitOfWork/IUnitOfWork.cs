using PetTag.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.UnitOfWork
{
    public interface IUnitOfWork
    {
        IPetRepo PetRepo { get; }
        IPetOwnerRepo PetOwnerRepo { get; }
        IPetChipRepo PetChipRepo { get; }
        IVetRepo VetRepo { get; }
        IVetAppointmentRepo VetAppointmentRepo { get; }
        IAlertRepo AlertRepo { get; }
        IHealthRecordRepo HealthRecordRepo { get; }
        IActivityLogRepo ActivityLogRepo { get; }
    }
}
