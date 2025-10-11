using System;
using System.Collections.Generic;
using PetTag.Service.DTOs;

namespace PetTag.Service.Interfaces
{
    public interface IVetAppointmentService
    {
        
        IList<VetAppointmentListItemDto> GetAllByPet(int petId, DateTime? start = null, DateTime? end = null);
        IList<VetAppointmentListItemDto> GetAllByVet(int vetId, DateTime? start = null, DateTime? end = null);
        IList<VetAppointmentListItemDto> GetByDateRange(DateTime start, DateTime end);
        VetAppointmentDetailDto? Get(int id);

        
        void Add(VetAppointmentCreateDto dto);
        void Update(int id, VetAppointmentUpdateDto dto);

       
        void Delete(int id);     
        void SoftDelete(int id);  
        void UndoDelete(int id); 
    }
}

