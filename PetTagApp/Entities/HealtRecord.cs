using PetTag.Core.BaseEntities;
using PetTag.Core.Enums;
using PetTag.Core.Exceptions;

namespace PetTag.Core.Entities
{
    public class HealtRecord : BaseEntity
    {
        public HealtRecord()
        {

        }

        public HealtRecord(string description, DateTime recordDate, bool isVaccination, string? treatment, int petId)
        {
            Description = description; // Validation burada çalışır
            RecordDate = recordDate;
            IsVaccination = isVaccination;
            Treatment = treatment;
            PetId = petId;
            CreateDate = DateTime.Now;
            Status = EntityStatus.Active;
        }

        private string? _description;
		public string? Description
		{
			get =>_description;
			set 
			{
                if (string.IsNullOrEmpty(value))
                    throw new EmptyHealthRecordDescriptionException();
                else
                    _description = value;
            }
		}
        public DateTime RecordDate { get; set; }
        public bool IsVaccination { get; set; }
        public string? Treatment { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
