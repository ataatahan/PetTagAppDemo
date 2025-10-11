using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IPetService
    {
        
        IList<PetListItemDto> GetAll();
        PetDetailDto? Get(int id);

        
        void Add(PetCreateDto dto);
        void Update(int id, PetUpdateDto dto);

        
        void Delete(int id);     
        void SoftDelete(int id); 
        void UndoDelete(int id); 
    }
}

