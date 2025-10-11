using PetTag.Core.Enums;
using static PetTag.Core.Exceptions.EntityStatusExceptions;

namespace PetTag.Core.BaseEntities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; protected set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }

        public EntityStatus Status { get; set; } = EntityStatus.Active;

        public void EntityAsActive()
        {
            if (Status == EntityStatus.Pasive)
            {
                Status = EntityStatus.Active;
                UpdateDate = DateTime.Now;
            }
            else
                throw new EntityAlreadyActiveException();
        }
        public void EntityAsPassive()
        {
            if (Status == EntityStatus.Active)
            {
                Status = EntityStatus.Pasive;
                UpdateDate = DateTime.Now;
            }
            else
                throw new EntityAlreadyPassiveException();
        }
    }
}
