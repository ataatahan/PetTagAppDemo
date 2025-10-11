using PetTag.Core.BaseEntities;
using PetTag.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Core.Entities
{
    public class Alert : BaseEntity
    {
        public Alert()
        {
            
        }
        public Alert(AlertType alertType, string message, int petId)
        {
            AlertType = alertType;
            Message = message;
            PetId = petId;
            AlertDate = DateTime.Now;
            Status = EntityStatus.Active; // BaseEntity'den geliyor
            CreateDate = DateTime.Now;
        }

        public AlertType? AlertType { get; set; }
        public string? Message { get; set; }
        public DateTime AlertDate { get; set; } = DateTime.Now;
        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
