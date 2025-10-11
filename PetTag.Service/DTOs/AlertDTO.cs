using PetTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    
    public readonly record struct AlertListItemDto(
        int Id,
        DateTime AlertDate,
        AlertType? AlertType,
        string? Message,
        int PetId
    );

   
    public readonly record struct AlertDetailDto(
        int Id,
        DateTime AlertDate,
        AlertType? AlertType,
        string? Message,
        int PetId
    );

        public class AlertCreateDto
    {
        public required int PetId { get; set; }
        public required AlertType AlertType { get; set; }
        public required string Message { get; set; }
        public DateTime? AlertDate { get; set; } 
    }

    
    public class AlertUpdateDto
    {
        public int? PetId { get; set; }
        public AlertType? AlertType { get; set; }
        public string? Message { get; set; }
        public DateTime? AlertDate { get; set; }
    }
}
