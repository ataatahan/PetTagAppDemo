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
    public class PetChipService : IPetChipService
    {
        private readonly IPetChipRepo _repo;

        public PetChipService(IPetChipRepo repo)
        {
            _repo = repo;
        }
        public PetChipService(IUnitOfWork uow) : this(uow.PetChipRepo) { }

        //okuma kısmı
        public PetChipDetailDto? Get(int id)
        {
            var e = _repo.GetById(id);
            return e is null ? null : ToDetailDto(e);
        }

        public PetChipDetailDto? GetByPetId(int petId)
        {
            var e = _repo.GetChipByPetId(petId);
            return e is null ? null : ToDetailDto(e);
        }

        public PetChipDetailDto? GetByNumber(Guid chipNumber)
        {
            var e = _repo.GetChipByNumber(chipNumber);
            return e is null ? null : ToDetailDto(e);
        }

        public IList<PetChipListItemDto> GetActive()
        {
            return _repo.GetActiveChips()
                        .OrderByDescending(x => x.CreateDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public IList<PetChipListItemDto> GetPassive()
        {
            return _repo.GetPassiveChips()
                        .OrderByDescending(x => x.CreateDate)
                        .Select(ToListDto)
                        .ToList();
        }

        //yazma kısmı
        public void Add(PetChipCreateDto dto)
        {
            // 1) Chip oluştur
            var entity = new PetChip(dto.PetId);

            // 2) Konumu DTO verdiyse elle, vermediyse otomatik rastgele ata
            if (dto.LastLatitude.HasValue && dto.LastLongitude.HasValue)
                entity.SetLocation(dto.LastLatitude.Value, dto.LastLongitude.Value, dto.LastLocationAtUtc);
            else
                entity.SetLocation(); // İSTANBUL bbox içinde random

            // 3) Kaydet
            var ok = _repo.Add(entity);
            if (!ok) throw new Exception("PetChip eklenemedi.");
        }

        public void Update(int id, PetChipUpdateDto dto)
        {
            var chip = _repo.GetById(id) ?? throw new Exception("PetChip not found");

            if (dto.PetId.HasValue) chip.PetId = dto.PetId.Value;
            if (dto.ChipNumber.HasValue) chip.ChipNumber = dto.ChipNumber.Value;

            // ChipStatus ü burada değiştiriyor statusu kullanarak 
            if (dto.ChipStatus.HasValue)
            {
                if (dto.ChipStatus.Value == ChipStatus.Active && chip.ChipStatus == ChipStatus.Pasive)
                    chip.ChipAsActive();
                else if (dto.ChipStatus.Value == ChipStatus.Pasive && chip.ChipStatus == ChipStatus.Active)
                    chip.ChipAsPassive();
                
            }

            // Konum güncellemesi (ikisi birden gelmeli)
            if (dto.LastLatitude.HasValue && dto.LastLongitude.HasValue)
                chip.SetLocation(dto.LastLatitude.Value, dto.LastLongitude.Value, dto.LastLocationAtUtc);

            if (dto.DeleteDate.HasValue) chip.DeleteDate = dto.DeleteDate;

            _repo.Update(chip); // içeride SaveChanges()
        }

        // -------- STATUS / LOCATION --------
        public void Activate(int id)
        {
            var chip = _repo.GetById(id) ?? throw new Exception("PetChip not found");
            if (chip.ChipStatus == ChipStatus.Pasive)
            {
                chip.ChipAsActive();
                _repo.Update(chip);
            }
        }

        public void Deactivate(int id)
        {
            var chip = _repo.GetById(id) ?? throw new Exception("PetChip not found");
            if (chip.ChipStatus == ChipStatus.Active)
            {
                chip.ChipAsPassive();
                _repo.Update(chip);
            }
        }

        public void SetLocation(int id, decimal latitude, decimal longitude, DateTime? whenUtc = null)
        {
            var chip = _repo.GetById(id) ?? throw new Exception("Chip bulunamadı.");
            chip.SetLocation(latitude, longitude, whenUtc);  // entity method (manuel)
            if (!_repo.Update(chip)) throw new Exception("Konum ayarlanamadı.");
        }

        public void SetRandomLocation(int id)
        {
            var chip = _repo.GetById(id) ?? throw new Exception("PetChip not found");
            chip.SetLocation(); // İstanbul bbox'ı içinde random
            _repo.Update(chip);
        }

        // silme işlemleri
        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);

        // -------- Mapping helpers --------
        private static PetChipListItemDto ToListDto(PetChip e) =>
            new PetChipListItemDto(
                e.Id, e.PetId, e.ChipNumber, e.ChipStatus,
                e.LastLatitude, e.LastLongitude, e.LastLocationAtUtc, e.DeleteDate
            );

        private static PetChipDetailDto ToDetailDto(PetChip e) =>
            new PetChipDetailDto(
                e.Id, e.PetId, e.ChipNumber, e.ChipStatus,
                e.LastLatitude, e.LastLongitude, e.LastLocationAtUtc, e.DeleteDate
            );

        public void RandomizeLocation(int id)
        {
            var chip = _repo.GetById(id);       // kendi repo metoduna göre uyumla
            if (chip is null) throw new Exception("Chip bulunamadı.");

            chip.SetLocation();                 
            var ok = _repo.Update(chip);
            if (!ok) throw new Exception("Konum rastgele ayarlanamadı.");
        }
    }
}
