using Events.Domain.Entities;
using Events.Infrastructure.Contexts;
using Events.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;


namespace Events.Infrastructure.Repositories
{
    public class UnitOfWorkBase : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IRepositoryBase<ApplicationUser> _applicationUserRepository;
        public IRepositoryBase<ApplicationUser> ApplicationUserRepository => _applicationUserRepository ?? (_applicationUserRepository = new RepositoryBase<ApplicationUser>(_context));

        public IRepositoryBase<EventPlace> _eventPlaceRepository;
        public IRepositoryBase<EventPlace> EventPlaceRepository => _eventPlaceRepository ?? (_eventPlaceRepository = new RepositoryBase<EventPlace>(_context));
        public IRepositoryBase<SeatsType> _seatsTypeRepository;
        public IRepositoryBase<SeatsType> SeatsTypeRepository => _seatsTypeRepository ?? (_seatsTypeRepository = new RepositoryBase<SeatsType>(_context));

        public IRepositoryBase<TitleFirst> _titleFirstRepository;
        public IRepositoryBase<TitleFirst> TitleFirstRepository => _titleFirstRepository ?? (_titleFirstRepository = new RepositoryBase<TitleFirst>(_context));
        public IRepositoryBase<TitleSecond> _titleSecondRepository;
        public IRepositoryBase<TitleSecond> TitleSecondRepository => _titleSecondRepository ?? (_titleSecondRepository = new RepositoryBase<TitleSecond>(_context));
        public IRepositoryBase<PersonType> _personTypeRepository;
        public IRepositoryBase<PersonType> PersonTypeRepository => _personTypeRepository ?? (_personTypeRepository = new RepositoryBase<PersonType>(_context));
        public IRepositoryBase<Event> _eventRepository;
        public IRepositoryBase<Event> EventRepository => _eventRepository ?? (_eventRepository = new RepositoryBase<Event>(_context));
        public IRepositoryBase<Invitation> _invitationRepository;
        public IRepositoryBase<Invitation> InvitationRepository => _invitationRepository ?? (_invitationRepository = new RepositoryBase<Invitation>(_context));
        public IRepositoryBase<EventSeat> _eventSeatRepository;
        public IRepositoryBase<EventSeat> EventSeatRepository => _eventSeatRepository ?? (_eventSeatRepository = new RepositoryBase<EventSeat>(_context));

        public IRepositoryBase<PlaceSeat> _placeSeatRepository;
        public IRepositoryBase<PlaceSeat> PlaceSeatRepository => _placeSeatRepository ?? (_placeSeatRepository = new RepositoryBase<PlaceSeat>(_context));

        public UnitOfWorkBase(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Commit()
        {
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RollBack()
        {
            foreach (var entry in _context.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }


    }
}
