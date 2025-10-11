using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    
    public readonly record struct PetOwnerListItemDto(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string FullName 
    );

    
    public readonly record struct PetOwnerDetailDto(
        int Id,
        string FirstName,
        string LastName,
        string Email,
        string FullName
   
    );

   
    public class PetOwnerCreateDto
    {
        public required string FirstName { get; set; }  
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }

    
    public class PetOwnerUpdateDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
    }
}
