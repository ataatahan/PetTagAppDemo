using System;
using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IPetChipService
    {
        
        PetChipDetailDto? Get(int id);
        PetChipDetailDto? GetByPetId(int petId);
        PetChipDetailDto? GetByNumber(Guid chipNumber);
        IList<PetChipListItemDto> GetActive();
        IList<PetChipListItemDto> GetPassive();

        
        void Add(PetChipCreateDto dto);
        void Update(int id, PetChipUpdateDto dto);

        // Durum/Konum
        void Activate(int id);
        void Deactivate(int id);
        void SetLocation(int id, decimal latitude, decimal longitude, DateTime? whenUtc = null);
        void SetRandomLocation(int id);

        // silme
        void Delete(int id);      // kalıcı sil
        void SoftDelete(int id);  // pasifle
        void UndoDelete(int id);  // tekrar aktive et

        void RandomizeLocation(int id);                                 // entity.SetLocation()
        
    }
}
