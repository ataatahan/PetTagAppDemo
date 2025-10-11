using PetTag.Repo.UnitOfWork;       
using PetTag.Service;               
using PetTag.Service.Concretes;
using PetTag.Service.Interfaces;
using System;                      

namespace PetTag.Service.UnitOfWorks
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly Lazy<IActivityLogService> _activityLogs;
        private readonly Lazy<IAlertService> _alerts;
        private readonly Lazy<IHealthRecordService> _healthRecords;
        private readonly Lazy<IPetChipService> _petChips;
        private readonly Lazy<IPetOwnerService> _petOwners;
        private readonly Lazy<IPetService> _pets;
        private readonly Lazy<IVetAppointmentService> _vetAppointments;
        private readonly Lazy<IVetService> _vets;

        public UnitOfWorkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            _activityLogs = new Lazy<IActivityLogService>(() => new ActivityLogService(_unitOfWork));
            _alerts = new Lazy<IAlertService>(() => new AlertService(_unitOfWork));
            _healthRecords = new Lazy<IHealthRecordService>(() => new HealthRecordService(_unitOfWork));
            _petChips = new Lazy<IPetChipService>(() => new PetChipService(_unitOfWork));
            _petOwners = new Lazy<IPetOwnerService>(() => new PetOwnerService(_unitOfWork));
            _pets = new Lazy<IPetService>(() => new PetService(_unitOfWork));
            _vetAppointments = new Lazy<IVetAppointmentService>(() => new VetAppointmentService(_unitOfWork));
            _vets = new Lazy<IVetService>(() => new VetService(_unitOfWork));
        }

        public IActivityLogService ActivityLogs => _activityLogs.Value;
        public IAlertService Alerts => _alerts.Value;
        public IHealthRecordService HealthRecords => _healthRecords.Value;
        public IPetChipService PetChips => _petChips.Value;
        public IPetOwnerService PetOwners => _petOwners.Value;
        public IPetService Pets => _pets.Value;
        public IVetAppointmentService VetAppointments => _vetAppointments.Value;
        public IVetService Vets => _vets.Value;
    }
}
