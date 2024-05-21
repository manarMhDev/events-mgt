

using Events.Domain.Entities;

namespace Events.Domain.BaseEntities
{
    public class BaseEntity : IBaseEntity
    {
        public string? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual ApplicationUser CreatedBy { get; set; }
        public virtual ApplicationUser LastModifiedBy { get; set; }

        public void EntityCreated(string userId, DateTime creationTime)
        {
            CreatedById = userId;
            CreatedAt = creationTime;
            IsActive = true;
            LastModifiedAt = null;
            LastModifiedById = null;
        }

        public void EntityUpdated(string userId, DateTime dateTime, bool isActivated)
        {
            LastModifiedById = userId;
            LastModifiedAt = dateTime;
            IsActive = isActivated;
        }
    }
}
