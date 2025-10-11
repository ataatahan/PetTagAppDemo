using System;
using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IPetOwnerService
    {
        
        IList<PetOwnerListItemDto> GetAll(string? q = null, int page = 1, int pageSize = 20);
        PetOwnerDetailDto? Get(int id);
        PetOwnerDetailDto? GetByEmail(string email);

       
        void Add(PetOwnerCreateDto dto);
        void Update(int id, PetOwnerUpdateDto dto);

       
        void Delete(int id);      
        void SoftDelete(int id); 
        void UndoDelete(int id); 
    }
}
