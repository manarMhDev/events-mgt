

using Events.Application.Common.Interfaces.Authentication;
using Events.Application.SeatsTypes.Commands;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.Seats.Commands
{
    public class UpdateASeat : IRequest<Response<bool>>
    {
        private readonly UpdateASeatDto _updateSeatDto;
        public UpdateASeat(UpdateASeatDto updateSeatDto)
        {

            _updateSeatDto = updateSeatDto;

        }
        public class UpdateASeatHandler : BaseHandler, IRequestHandler<UpdateASeat, Response<bool>>
        {
            private readonly IFileService _documentService;
            public UpdateASeatHandler(IUnitOfWork iUnitOfWork,
                IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor,
                IFileService documentService) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
                _documentService = documentService;
            }

            public async Task<Response<bool>> Handle(UpdateASeat request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.PlaceSeatRepository.GetById(request._updateSeatDto.Id);
                if (obj is not null)
                    obj.UpdatePlaceSeat(request._updateSeatDto.EventPlaceId,
                        request._updateSeatDto.Code,
                        request._updateSeatDto.SeatTypeId);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateASeatDto
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public int EventPlaceId { get; set; }
            public int SeatTypeId { get; set; }
        }
    }
}
