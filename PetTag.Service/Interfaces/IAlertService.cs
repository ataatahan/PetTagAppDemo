using System;
using System.Collections.Generic;
using PetTag.Core.Enums;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IAlertService
    {
        // okuma
        IList<AlertListItemDto> GetAllByPet(int petId);
        IList<AlertListItemDto> GetByType(AlertType type);
        IList<AlertListItemDto> GetRecent(int lastDays = 7);
        AlertDetailDto? Get(int id);

        // yazma
        void Add(AlertCreateDto dto);
        void Update(int id, AlertUpdateDto dto);

        // silme
        void Delete(int id);      // kalıcı
        void SoftDelete(int id);  // pasif
        void UndoDelete(int id);  // tekrar aktif
    }
}
