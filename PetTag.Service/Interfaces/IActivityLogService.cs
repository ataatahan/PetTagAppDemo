using System;
using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IActivityLogService
    {
        // Belirli pet için tüm logları getirecek 
        IList<ActivityLogListItemDto> GetAllByPet(int petId, DateTime? start = null, DateTime? end = null);
        IList<ActivityLogListItemDto> GetByDateRange(DateTime start, DateTime end);

        // Tek kayıt (detay)
        ActivityLogDetailDto? Get(int id);

        // Ekleme
        void Add(ActivityLogCreateDto dto);

        // Güncelleme 
        void Update(int id, ActivityLogUpdateDto dto);

        // Silme seçenekleri
        void Delete(int id);      // kalıcısil
        void SoftDelete(int id);  // pasifle
        void UndoDelete(int id);  // tekrar aktive et
    }
}
