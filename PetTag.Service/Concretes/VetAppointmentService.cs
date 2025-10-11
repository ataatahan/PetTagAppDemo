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
    public class VetAppointmentService : IVetAppointmentService
    {
        private readonly IVetAppointmentRepo _repo;

        public VetAppointmentService(IVetAppointmentRepo repo)
        {
            _repo = repo;
        }

        public VetAppointmentService(IUnitOfWork uow) : this(uow.VetAppointmentRepo) { }

        // okuma
        public IList<VetAppointmentListItemDto> GetAllByPet(int petId, DateTime? start = null, DateTime? end = null)
        {
            if (start is null && end is null)
            {
                return _repo.GetAppointmentsByPetId(petId)
                            .OrderByDescending(a => a.AppointmentDate)
                            .Select(ToListDto)
                            .ToList();
            }

            var s = start ?? DateTime.MinValue;
            var e = end ?? DateTime.MaxValue;

            return _repo.GetAppointmentsByDateRange(s, e)
                        .Where(a => a.PetId == petId)
                        .OrderByDescending(a => a.AppointmentDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<VetAppointmentListItemDto> GetAllByVet(int vetId, DateTime? start = null, DateTime? end = null)
        {
            if (start is null && end is null)
            {
                return _repo.GetAppointmentsByVetId(vetId)
                            .OrderByDescending(a => a.AppointmentDate)
                            .Select(ToListDto)
                            .ToList();
            }

            var s = start ?? DateTime.MinValue;
            var e = end ?? DateTime.MaxValue;

            return _repo.GetAppointmentsByDateRange(s, e)
                        .Where(a => a.VetId == vetId)
                        .OrderByDescending(a => a.AppointmentDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<VetAppointmentListItemDto> GetByDateRange(DateTime start, DateTime end)
        {
            return _repo.GetAppointmentsByDateRange(start, end)
                        .OrderByDescending(a => a.AppointmentDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public VetAppointmentDetailDto? Get(int id)
        {
            var entity = _repo.GetById(id);
            return entity is null ? null : ToDetailDto(entity);
        }

        // yazma
        public void Add(VetAppointmentCreateDto dto)
        {
            var appt = new VetAppointment
            {
                AppointmentDate = dto.AppointmentDate,
                VetId = dto.VetId,
                PetId = dto.PetId,
                Notes = dto.Notes
            };

            _repo.Add(appt); // içeride SaveChanges()
        }

        public void Update(int id, VetAppointmentUpdateDto dto)
        {
            var appt = _repo.GetById(id) ?? throw new Exception("VetAppointment not found");

            if (dto.AppointmentDate.HasValue) appt.AppointmentDate = dto.AppointmentDate.Value;
            if (dto.VetId.HasValue) appt.VetId = dto.VetId.Value;
            if (dto.PetId.HasValue) appt.PetId = dto.PetId.Value;
            if (dto.Notes is not null) appt.Notes = dto.Notes;

            _repo.Update(appt); // içeride SaveChanges()
        }

        // silme
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);

        // -------- Mapping helpers --------
        private static VetAppointmentListItemDto ToListDto(VetAppointment a) =>
            new VetAppointmentListItemDto
            {
                Id = a.Id,
                AppointmentDate = a.AppointmentDate,
                VetId = a.VetId,
                PetId = a.PetId,
                Notes = a.Notes
            };

        private static VetAppointmentDetailDto ToDetailDto(VetAppointment a) =>
            new VetAppointmentDetailDto
            {
                Id = a.Id,
                AppointmentDate = a.AppointmentDate,
                VetId = a.VetId,
                PetId = a.PetId,
                Notes = a.Notes
            };
    }
}