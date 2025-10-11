using System;
using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IHealthRecordService
    {
        // okuma
        IList<HealtRecordListItemDto> GetAllByPet(int petId);
        IList<HealtRecordListItemDto> GetVaccinations();
        IList<HealtRecordListItemDto> GetByDateRange(DateTime start, DateTime end);
        HealtRecordDetailDto? Get(int id);

        // yazma
        void Add(HealtRecordCreateDto dto);
        void Update(int id, HealtRecordUpdateDto dto);

        // silme
        void Delete(int id);      // kalıcı
        void SoftDelete(int id);  // pasif
        void UndoDelete(int id);  // tekrar aktif
    }
}
