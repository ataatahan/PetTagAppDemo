using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    
    public readonly record struct HealtRecordListItemDto(
        int Id,
        DateTime RecordDate,
        bool IsVaccination,
        string? Description,
        string? Treatment,
        int PetId
    );

 
    public readonly record struct HealtRecordDetailDto(
        int Id,
        DateTime RecordDate,
        bool IsVaccination,
        string? Description,
        string? Treatment,
        int PetId
    );

    
    public class HealtRecordCreateDto
    {
        public required int PetId { get; set; }
        public required string Description { get; set; }   
        public required DateTime RecordDate { get; set; }
        public required bool IsVaccination { get; set; }
        public string? Treatment { get; set; }
    }

    
    public class HealtRecordUpdateDto
    {
        public int? PetId { get; set; }
        public string? Description { get; set; }
        public DateTime? RecordDate { get; set; }
        public bool? IsVaccination { get; set; }
        public string? Treatment { get; set; }
    }
}
