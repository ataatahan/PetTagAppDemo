using PetTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    
    public readonly record struct PetChipListItemDto(
        int Id,
        int PetId,
        Guid ChipNumber,
        ChipStatus ChipStatus,
        decimal? LastLatitude,
        decimal? LastLongitude,
        DateTime? LastLocationAtUtc,
        DateTime? DeleteDate
    );

    
    public readonly record struct PetChipDetailDto(
        int Id,
        int PetId,
        Guid ChipNumber,
        ChipStatus ChipStatus,
        decimal? LastLatitude,
        decimal? LastLongitude,
        DateTime? LastLocationAtUtc,
        DateTime? DeleteDate
    );

    
    public class PetChipCreateDto
    {
        public required int PetId { get; set; }
        public Guid? ChipNumber { get; set; }          
        public decimal? LastLatitude { get; set; }      
        public decimal? LastLongitude { get; set; }
        public DateTime? LastLocationAtUtc { get; set; }
    }

   
    public class PetChipUpdateDto
    {
        public int? PetId { get; set; }
        public Guid? ChipNumber { get; set; }           
        public ChipStatus? ChipStatus { get; set; }  
        public decimal? LastLatitude { get; set; }
        public decimal? LastLongitude { get; set; }
        public DateTime? LastLocationAtUtc { get; set; }
        public DateTime? DeleteDate { get; set; }       
    }
}
