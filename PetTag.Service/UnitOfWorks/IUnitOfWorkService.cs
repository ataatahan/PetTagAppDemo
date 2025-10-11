using PetTag.Service.Interfaces;

namespace PetTag.Service.UnitOfWorks
{
    public interface IUnitOfWorkService
    {
        IActivityLogService ActivityLogs { get; }
        IAlertService Alerts { get; }
        IHealthRecordService HealthRecords { get; }
        IPetChipService PetChips { get; }
        IPetOwnerService PetOwners { get; }
        IPetService Pets { get; }
        IVetAppointmentService VetAppointments { get; }
        IVetService Vets { get; }
    }
}
