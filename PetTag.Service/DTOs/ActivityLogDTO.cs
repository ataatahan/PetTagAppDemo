using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Service.DTOs
{
    // listeleme işlemleri için bunu kullanacağız abe
    public readonly record struct ActivityLogListItemDto(
        int Id,
        DateTime LogDate,
        double? WalkingMinutes,
        double? RunningMinutes,
        double? SleepingMinutes,
        double? Temperature,
        double? Distance,
        int PetId
    );

    // DETAIL için bunu kullanacaz
    public readonly record struct ActivityLogDetailDto(
        int Id,
        DateTime LogDate,
        double? WalkingMinutes,
        double? RunningMinutes,
        double? SleepingMinutes,
        double? Temperature,
        double? Distance,
        int PetId
    );

    // veri girişi için yani create işlemleri için bunu kullanacaz 
    public class ActivityLogCreateDto
    {
        public required int PetId { get; set; }               
        public double? WalkingMinutes { get; set; }
        public double? RunningMinutes { get; set; }
        public double? SleepingMinutes { get; set; }
        public double? Temperature { get; set; }
        public double? Distance { get; set; }
        public DateTime? LogDate { get; set; }               
    }

    // UPDATE  Hepsi opsiyonel gelenler neyse onu uygular
    public class ActivityLogUpdateDto
    {
        public int? PetId { get; set; }
        public double? WalkingMinutes { get; set; }
        public double? RunningMinutes { get; set; }
        public double? SleepingMinutes { get; set; }
        public double? Temperature { get; set; }
        public double? Distance { get; set; }
        public DateTime? LogDate { get; set; }
    }
}
