using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    public class VetAppointmentListItemDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int VetId { get; set; }
        public int PetId { get; set; }
        public string? Notes { get; set; }
    }

    public class VetAppointmentDetailDto
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int VetId { get; set; }
        public int PetId { get; set; }
        public string? Notes { get; set; }
    }

    public class VetAppointmentCreateDto
    {
        public DateTime AppointmentDate { get; set; }
        public int VetId { get; set; }
        public int PetId { get; set; }
        public string? Notes { get; set; }
    }

    public class VetAppointmentUpdateDto
    {
        public DateTime? AppointmentDate { get; set; }
        public int? VetId { get; set; }
        public int? PetId { get; set; }
        public string? Notes { get; set; }
    }
}