

using Events.Domain.Entities;

namespace Events.Domain.BaseEntities
{
    public interface IBaseEntity
    {
        public string? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string? LastModifiedById { get; set; }
        public DateTime? LastModifiedAt { get; set; }
        public bool IsActive { get; set; }
        void EntityCreated(string userId, DateTime creationTime);
        void EntityUpdated(string userId, DateTime dateTime, bool isActivated);
    }
}
