

using Events.Domain.BaseEntities;

namespace Events.Domain.Entities
{
    public class EventSeat : BaseEntity
    {
        public int Id { get; private set; }
        public int InvitationId { get; private set; }
        public int PlaceSeatId { get; private set; }
        public virtual Invitation Invitation { get; private set; }
        public virtual PlaceSeat PlaceSeat { get; private set; }
        public EventSeat()
        {

        }

        public EventSeat(int invitationId, int placeSeatId)
        {
            InvitationId = invitationId;
            PlaceSeatId = placeSeatId;
        }
        public void UpdateEventSeat(int placeSeatId)
        {
            PlaceSeatId = placeSeatId;
        }
    }
}
