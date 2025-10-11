using PetTag.Core.Entities;
using PetTag.Repo.Interfaces;
using PetTag.Repo.UnitOfWork;
using PetTag.Service.DTOs;
using PetTag.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.Concretes
{
    public class HealthRecordService : IHealthRecordService
    {
        private readonly IHealthRecordRepo _repo;

        public HealthRecordService(IHealthRecordRepo repo)
        {
            _repo = repo;
        }
        public HealthRecordService(IUnitOfWork uow) : this(uow.HealthRecordRepo) { }

        // okuma kısmı
        public IList<HealtRecordListItemDto> GetAllByPet(int petId)
        {
            return _repo.GetRecordsByPetId(petId)
                        .OrderByDescending(h => h.RecordDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<HealtRecordListItemDto> GetVaccinations()
        {
            return _repo.GetVaccinationRecords()
                        .OrderByDescending(h => h.RecordDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<HealtRecordListItemDto> GetByDateRange(DateTime start, DateTime end)
        {
            return _repo.GetRecordsByDateRange(start, end)
                        .OrderByDescending(h => h.RecordDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public HealtRecordDetailDto? Get(int id)
        {
            var entity = _repo.GetById(id);
            return entity is null ? (HealtRecordDetailDto?)null : ToDetailDto(entity);
        }

        // yazma kısmı
        public void Add(HealtRecordCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Description))
                throw new ArgumentException("Description cannot be empty.", nameof(dto.Description));

            var rec = new HealtRecord(
                description: dto.Description,
                recordDate: dto.RecordDate,
                isVaccination: dto.IsVaccination,
                treatment: dto.Treatment,
                petId: dto.PetId
            );

            _repo.Add(rec); // eski repo stili: içeride SaveChanges()
        }

        public void Update(int id, HealtRecordUpdateDto dto)
        {
            var rec = _repo.GetById(id) ?? throw new Exception("HealtRecord not found");

            if (dto.Description is not null)
            {
                if (string.IsNullOrWhiteSpace(dto.Description))
                    throw new ArgumentException("Description cannot be empty.", nameof(dto.Description));
                rec.Description = dto.Description; // entity setter'ı domain kuralını korur
            }

            if (dto.RecordDate.HasValue) rec.RecordDate = dto.RecordDate.Value;
            if (dto.IsVaccination.HasValue) rec.IsVaccination = dto.IsVaccination.Value;
            if (dto.Treatment is not null) rec.Treatment = dto.Treatment;
            if (dto.PetId.HasValue) rec.PetId = dto.PetId.Value;

            _repo.Update(rec); // içeride SaveChanges()
        }

        // silme kısmı
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);

        // ---------- Mapping helpers ----------
        private static HealtRecordListItemDto ToListDto(HealtRecord h) =>
            new HealtRecordListItemDto(
                h.Id, h.RecordDate, h.IsVaccination, h.Description, h.Treatment, h.PetId
            );

        private static HealtRecordDetailDto ToDetailDto(HealtRecord h) =>
            new HealtRecordDetailDto(
                h.Id, h.RecordDate, h.IsVaccination, h.Description, h.Treatment, h.PetId
            );
    }
}
