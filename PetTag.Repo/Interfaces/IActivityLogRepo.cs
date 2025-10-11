using PetTag.Core.Entities;

namespace PetTag.Repo.Interfaces
{
    public interface IActivityLogRepo : IGenericRepo<ActivityLog>
    {
        ICollection<ActivityLog> GetLogsByPetId(int petId);
        ICollection<ActivityLog> GetLogsByDateRange(DateTime start, DateTime end);
    }
}
