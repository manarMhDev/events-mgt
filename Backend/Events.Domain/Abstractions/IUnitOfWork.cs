

using Events.Domain.Entities;
using Events.Infrastructure.Repositories;

namespace Events.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork :  IDisposable
    {
        IRepositoryBase<ApplicationUser> ApplicationUserRepository { get; }
        IRepositoryBase<EventPlace> EventPlaceRepository { get; }
        IRepositoryBase<SeatsType> SeatsTypeRepository { get; }
        IRepositoryBase<TitleFirst> TitleFirstRepository { get; }
        IRepositoryBase<TitleSecond> TitleSecondRepository { get; }
        IRepositoryBase<PersonType> PersonTypeRepository { get; }
        IRepositoryBase<Event> EventRepository { get; }
        IRepositoryBase<Invitation> InvitationRepository { get; }
        IRepositoryBase<EventSeat> EventSeatRepository { get; }
        IRepositoryBase<PlaceSeat> PlaceSeatRepository { get; }
        bool Commit();
        Task<bool> CommitAsync();
        void RollBack();
    }
}
