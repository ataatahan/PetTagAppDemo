using PetTag.Core.Entities;
using PetTag.Core.Enums;
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
    public class AlertService : IAlertService
    {
        private readonly IAlertRepo _repo;

        public AlertService(IAlertRepo repo)
        {
            _repo = repo;
        }
        public AlertService(IUnitOfWork uow) : this(uow.AlertRepo) { }

        // okuma kısmı
        public IList<AlertListItemDto> GetAllByPet(int petId)
        {
            return _repo.GetAlertsByPetId(petId)
                        .OrderByDescending(a => a.AlertDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<AlertListItemDto> GetByType(AlertType type)
        {
            return _repo.GetAlertsByType(type)
                        .OrderByDescending(a => a.AlertDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<AlertListItemDto> GetRecent(int lastDays = 7)
        {
            return _repo.GetRecentAlerts(lastDays)
                        .OrderByDescending(a => a.AlertDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public AlertDetailDto? Get(int id)
        {
            var entity = _repo.GetById(id);
            return entity is null ? (AlertDetailDto?)null : ToDetailDto(entity);
        }

        //yazma kısmı
        public void Add(AlertCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new ArgumentException("Message cannot be empty.", nameof(dto.Message));

            var alert = new Alert(dto.AlertType, dto.Message, dto.PetId);
            if (dto.AlertDate.HasValue)
                alert.AlertDate = dto.AlertDate.Value;

            _repo.Add(alert); // eski repo stili: içeride SaveChanges()
        }

        public void Update(int id, AlertUpdateDto dto)
        {
            var alert = _repo.GetById(id) ?? throw new Exception("Alert not found");

            if (dto.PetId.HasValue) alert.PetId = dto.PetId.Value;
            if (dto.AlertType.HasValue) alert.AlertType = dto.AlertType.Value;
            if (dto.Message is not null)
            {
                if (string.IsNullOrWhiteSpace(dto.Message))
                    throw new ArgumentException("Message cannot be empty.", nameof(dto.Message));
                alert.Message = dto.Message;
            }
            if (dto.AlertDate.HasValue) alert.AlertDate = dto.AlertDate.Value;

            _repo.Update(alert); // içeride SaveChanges()
        }

        // silme kısmı
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);

        // -------- Mapping helpers --------
        private static AlertListItemDto ToListDto(Alert a) =>
            new AlertListItemDto(a.Id, a.AlertDate, a.AlertType, a.Message, a.PetId);

        private static AlertDetailDto ToDetailDto(Alert a) =>
            new AlertDetailDto(a.Id, a.AlertDate, a.AlertType, a.Message, a.PetId);
    }
}
