using PetTag.Repo.Concretes;
using PetTag.Repo.Concreties;
using PetTag.Repo.Contexts;
using PetTag.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PetTagAppDbContext _context;

        private readonly Lazy<IPetRepo> _petRepo;
        private readonly Lazy<IPetOwnerRepo> _petOwnerRepo;
        private readonly Lazy<IPetChipRepo> _petChipRepo;
        private readonly Lazy<IVetRepo> _vetRepo;
        private readonly Lazy<IVetAppointmentRepo> _vetAppointmentRepo;
        private readonly Lazy<IAlertRepo> _alertRepo;
        private readonly Lazy<IHealthRecordRepo> _healthRecordRepo;
        private readonly Lazy<IActivityLogRepo> _activityLogRepo;

        public UnitOfWork(PetTagAppDbContext context)
        {
            _context = context;

            _petRepo = new Lazy<IPetRepo>(() => new PetRepo(_context));
            _petOwnerRepo = new Lazy<IPetOwnerRepo>(() => new PetOwnerRepo(_context));
            _petChipRepo = new Lazy<IPetChipRepo>(() => new PetChipRepo(_context));
            _vetRepo = new Lazy<IVetRepo>(() => new VetRepo(_context));
            _vetAppointmentRepo = new Lazy<IVetAppointmentRepo>(() => new VetAppointmentRepo(_context));
            _alertRepo = new Lazy<IAlertRepo>(() => new AlertRepo(_context));
            _healthRecordRepo = new Lazy<IHealthRecordRepo>(() => new HealthRecordRepo(_context));
            _activityLogRepo = new Lazy<IActivityLogRepo>(() => new ActivityLogRepo(_context));
        }

        public IPetRepo PetRepo => _petRepo.Value;
        public IPetOwnerRepo PetOwnerRepo => _petOwnerRepo.Value;
        public IPetChipRepo PetChipRepo => _petChipRepo.Value;
        public IVetRepo VetRepo => _vetRepo.Value;
        public IVetAppointmentRepo VetAppointmentRepo => _vetAppointmentRepo.Value;
        public IAlertRepo AlertRepo => _alertRepo.Value;
        public IHealthRecordRepo HealthRecordRepo => _healthRecordRepo.Value;
        public IActivityLogRepo ActivityLogRepo => _activityLogRepo.Value;

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
