

using Events.Domain.BaseEntities;
using Events.Domain.Enums;

namespace Events.Domain.Entities
{
    public class Event : BaseEntity
    {
        public int Id { get; private set; }
        public string NameArabic { get; private set; }
        public string NameEnglish { get; private set; }
        public string Description { get; private set; }
        public  int EventPlaceId { get; private set; }
        public DateTime EventDate { get; private set; }
        public virtual EventPlace EventPlace { get;  set; }
        public virtual List<Invitation> Invitations { get; set; }
        public Event() { }
        public Event(string nameArabic,  string nameEnglish, string description,  DateTime eventDate, int eventPlaceId) 
        {
            NameArabic = nameArabic;
            NameEnglish = nameEnglish;
            Description = description;
            EventPlaceId = eventPlaceId;
            EventDate = eventDate;
        }
        public void UpdateEvent(string nameArabic, string nameEnglish, string description, DateTime eventDate, int eventPlaceId)
        {
            NameArabic = nameArabic;
            NameEnglish = nameEnglish;
            Description = description;
            EventPlaceId = eventPlaceId;
            EventDate = eventDate;
        }

    }
}
