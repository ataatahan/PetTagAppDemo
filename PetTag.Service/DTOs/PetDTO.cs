using PetTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    
    public readonly record struct PetListItemDto(
        int Id,
        string Name,
        PetType Type,
        int Age,
        double? Weight,
        int PetOwnerId,
        int VetId,
        bool HasChip 
    );

    
    public readonly record struct PetDetailDto(
        int Id,
        string Name,
        PetType Type,
        int Age,
        double? Weight,
        int PetOwnerId,
        int VetId,
        bool HasChip

    );

    
    public class PetCreateDto
    {
        public required string Name { get; set; }
        public required PetType Type { get; set; }
        public required int Age { get; set; }          
        public double? Weight { get; set; }            
        public required int PetOwnerId { get; set; }
        public required int VetId { get; set; }
            
    }

   
    public class PetUpdateDto
    {
        public string? Name { get; set; }
        public PetType? Type { get; set; }
        public int? Age { get; set; }
        public double? Weight { get; set; }
        public int? PetOwnerId { get; set; }
        public int? VetId { get; set; }
            
    }
}
