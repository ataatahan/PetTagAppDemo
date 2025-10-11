using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IVetService
    {
       
        IList<VetListItemDto> GetAll(string? q = null, int page = 1, int pageSize = 20);
        VetDetailDto? Get(int id);
        VetDetailDto? GetByFullName(string fullName);

        
        void Add(VetCreateDto dto);
        void Update(int id, VetUpdateDto dto);

        
        void Delete(int id);      
        void SoftDelete(int id); 
        void UndoDelete(int id);  
    }
}
