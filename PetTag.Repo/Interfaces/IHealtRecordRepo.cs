using PetTag.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetTag.Repo.Interfaces
{
    public interface IHealthRecordRepo : IGenericRepo<HealtRecord>
    {
        ICollection<HealtRecord> GetRecordsByPetId(int petId);
        ICollection<HealtRecord> GetVaccinationRecords();
        ICollection<HealtRecord> GetRecordsByDateRange(DateTime start, DateTime end);
    }

}
