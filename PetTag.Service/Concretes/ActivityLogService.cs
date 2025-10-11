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
    public class ActivityLogService : IActivityLogService
    {
        private readonly IActivityLogRepo _repo;

        public ActivityLogService(IActivityLogRepo repo)
        {
            _repo = repo;
        }
        public ActivityLogService(IUnitOfWork uow) : this(uow.ActivityLogRepo) { }

        // okuma kısmı burada yapıldı cnm

        public IList<ActivityLogListItemDto> GetAllByPet(int petId, DateTime? start = null, DateTime? end = null)
        {
            
            // repodaki iki tane hazır metod ile yapıldı 
            
            if (start is null && end is null)
            {
                return _repo.GetLogsByPetId(petId)
                            .OrderByDescending(x => x.LogDate)
                            .Select(ToListDto)
                            .ToList();
            }

            var s = start ?? DateTime.MinValue;
            var e = end ?? DateTime.MaxValue;

            return _repo.GetLogsByDateRange(s, e)
                        .Where(x => x.PetId == petId)
                        .OrderByDescending(x => x.LogDate)
                        .Select(ToListDto)
                        .ToList();
        }

        public ActivityLogDetailDto? Get(int id)
        {
            var entity = _repo.GetById(id);
            return entity is null ? (ActivityLogDetailDto?)null : ToDetailDto(entity);
        }

        // Yazma kısmı burada yapılsı yani başlangıcı burası

        public void Add(ActivityLogCreateDto dto)
        {
            // Domain ctor’unda Validate çağrılıyor
            var log = new ActivityLog(
                petId: dto.PetId,
                walkingMinutes: dto.WalkingMinutes,
                runningMinutes: dto.RunningMinutes,
                sleepingMinutes: dto.SleepingMinutes,
                temperature: dto.Temperature,
                distance: dto.Distance
            );

            if (dto.LogDate.HasValue)
                log.LogDate = dto.LogDate.Value;

            _repo.Add(log); // Eski repo stili: içinde SaveChanges() var
        }

        public void Update(int id, ActivityLogUpdateDto dto)
        {
            var log = _repo.GetById(id) ?? throw new Exception("ActivityLog not found");

            // Entity’de setter’lar public; ctor’daki Validate tekrar etmiyor.
            // Bu nedenle güncelleme için aynı kuralları servis katmanında koruyalım:
            ValidateForUpdate(
                dto.WalkingMinutes ?? log.WalkingMinutes,
                dto.RunningMinutes ?? log.RunningMinutes,
                dto.SleepingMinutes ?? log.SleepingMinutes,
                dto.Temperature ?? log.Temperature,
                dto.Distance ?? log.Distance
            );

            if (dto.PetId.HasValue) log.PetId = dto.PetId.Value;
            if (dto.WalkingMinutes.HasValue) log.WalkingMinutes = dto.WalkingMinutes;
            if (dto.RunningMinutes.HasValue) log.RunningMinutes = dto.RunningMinutes;
            if (dto.SleepingMinutes.HasValue) log.SleepingMinutes = dto.SleepingMinutes;
            if (dto.Temperature.HasValue) log.Temperature = dto.Temperature;
            if (dto.Distance.HasValue) log.Distance = dto.Distance;
            if (dto.LogDate.HasValue) log.LogDate = dto.LogDate.Value;

            _repo.Update(log); // İçeride SaveChanges()
        }

        public void Delete(int id) => _repo.Delete(id);
        public void SoftDelete(int id) => _repo.SoftDelete(id);
        public void UndoDelete(int id) => _repo.UndoDelete(id);

        // --- Helpers: mapping & validation ---

        private static ActivityLogListItemDto ToListDto(ActivityLog l) =>
            new ActivityLogListItemDto(
                l.Id, l.LogDate, l.WalkingMinutes, l.RunningMinutes,
                l.SleepingMinutes, l.Temperature, l.Distance, l.PetId
            );

        private static ActivityLogDetailDto ToDetailDto(ActivityLog l) =>
            new ActivityLogDetailDto(
                l.Id, l.LogDate, l.WalkingMinutes, l.RunningMinutes,
                l.SleepingMinutes, l.Temperature, l.Distance, l.PetId
            );

        private static void ValidateForUpdate(double? walk, double? run, double? sleep, double? temp, double? dist)
        {
            if (walk < 0) throw new ArgumentOutOfRangeException(nameof(walk), "WalkingMinutes negatif olamaz.");
            if (run < 0) throw new ArgumentOutOfRangeException(nameof(run), "RunningMinutes negatif olamaz.");
            if (sleep > 48) throw new ArgumentOutOfRangeException(nameof(sleep), "SleepingMinutes 48 saati aşamaz.");
            if (sleep < 0 || sleep > 24) throw new ArgumentOutOfRangeException(nameof(sleep), "SleepingMinutes 0 ile 24 arasında olmalıdır.");
            if (temp < 0) throw new ArgumentOutOfRangeException(nameof(temp), "Sıcaklık negatif olamaz.");
            if (dist < 0 || dist > 1000) throw new ArgumentOutOfRangeException(nameof(dist), "Mesafe 0 ile 1000 arasında olmalıdır.");
        }
        public IList<ActivityLogListItemDto> GetByDateRange(DateTime start, DateTime end)
        {
            var list = _repo.GetLogsByDateRange(start, end);
            return list.Select(a => new ActivityLogListItemDto(
                a.Id,
                a.LogDate,
                a.WalkingMinutes,
                a.RunningMinutes,
                a.SleepingMinutes,
                a.Temperature,
                a.Distance,
                a.PetId
            )).ToList();
        }
    }
}
