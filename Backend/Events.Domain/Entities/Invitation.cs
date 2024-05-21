

using Events.Domain.BaseEntities;
using Events.Domain.Enums;
using System.Drawing;
using System.Xml.Linq;

namespace Events.Domain.Entities
{
    public class Invitation : BaseEntity
    {
        public int Id { get; private set; }
        public int EventId { get; private set; }
        public int? EventSeatId { get; private set; }
        public  int TitleFirstId { get; private set; }
        public int? TitleSecondId { get; private set; }
        public int PersonTypeId { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string? Whatsapp { get; private set; } = null;
        public string? Phone { get; private set; } = null;
        public string? Position { get; private set; } = null;
        public string? Party { get; private set; } = null;
        public bool SendWhatsapp { get; private set; }
        public bool SendEmail { get; private set; }
        public bool ConfirmAttendance { get; private set; }
        public Language Language { get; private set; }
        public InvitationStatus InvitationStatus { get; private set; }
        public FormType FormType { get; private set; }
        public virtual Event Event { get; set; }
        public virtual TitleFirst TitleFirst { get;  set; }
        public virtual TitleSecond TitleSecond { get;  set; }
        public virtual PersonType PersonType { get;  set; }
        public virtual EventSeat EventSeat { get; set; }
        public Invitation(
            int titleFirstId,
            int? titleSecondId,
            int personTypeId,
            int eventId,
            string fullName,
            string email,
            bool sendWhatsapp,
            bool sendEmail,
            bool confirmAttendance,
            Language language,
            InvitationStatus invitationStatus,
            FormType formType,
            string? whatsapp,
            string? phone,
            string? position,
            string? party)
        {
            TitleFirstId = titleFirstId;
            TitleSecondId = titleSecondId;
            PersonTypeId = personTypeId;
            EventId = eventId;
            FullName = fullName;
            Email = email;
            SendWhatsapp = sendWhatsapp;
            SendEmail = sendEmail;
            ConfirmAttendance = confirmAttendance;
            Language = language;
            InvitationStatus = invitationStatus;
            FormType = formType;
            Whatsapp = whatsapp;
            Phone = phone;
            Position = position;
            Party = party;

        }
        public void UpdateInvitation(int titleFirstId,
            int? titleSecondId,
            int personTypeId,
            int eventId,
            string fullName,
            string email,
            bool sendWhatsapp,
            bool sendEmail,
            bool confirmAttendance,
            Language language,
            InvitationStatus invitationStatus,
            FormType formType,
            string? whatsapp,
            string? phone,
            string? position,
            string? party)
        {
            TitleFirstId = titleFirstId;
            TitleSecondId = titleSecondId;
            PersonTypeId = personTypeId;
            EventId = eventId;
            FullName = fullName;
            Email = email;
            SendWhatsapp = sendWhatsapp;
            SendEmail = sendEmail;
            ConfirmAttendance = confirmAttendance;
            Language = language;
            InvitationStatus = invitationStatus;
            FormType = formType;
            Whatsapp = whatsapp;
            Phone = phone;
            Position = position;
            Party = party;

        }
        public void BookASeat(int seatId)
        {
            EventSeatId = seatId;
        }
        public void ConfirmAttend()
        {
            ConfirmAttendance = true;
        }
    }
   
}
