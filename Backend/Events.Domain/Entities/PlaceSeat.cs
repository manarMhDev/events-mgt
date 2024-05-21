

using Events.Domain.BaseEntities;

namespace Events.Domain.Entities
{
    public class PlaceSeat : BaseEntity
    {
        public int Id { get; private set; }
        public int EventPlaceId { get; private set; }
        public string Code { get; private set; }
        public int SeatTypeId { get; private set; }
        public virtual EventPlace EventPlace { get; set; }
        public virtual SeatsType SeatType { get; set; }
        public ICollection<EventSeat> EventSeats { get; set; }
        public PlaceSeat() { }

        public PlaceSeat(int eventPlaceId, string code, int seatTypeId)
        {
            EventPlaceId = eventPlaceId;
            Code = code;
            SeatTypeId = seatTypeId;
        }
        public void  UpdatePlaceSeat(int eventPlaceId, string code, int seatTypeId)
        {
            EventPlaceId = eventPlaceId;
            Code = code;
            SeatTypeId = seatTypeId;
        }
    }
}
