

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.SeatsTypes.Commands
{
    public class UpdateSeatsTypes : IRequest<Response<bool>>
    {
        private readonly UpdateSeatsTypeDto _updateSeatTypesDto;
        public UpdateSeatsTypes(UpdateSeatsTypeDto updateSeatTypesDto)
        {

            _updateSeatTypesDto = updateSeatTypesDto;

        }
        public class UpdateSeatsTypesHandler : BaseHandler, IRequestHandler<UpdateSeatsTypes, Response<bool>>
        {
            private readonly IFileService _documentService;
            public UpdateSeatsTypesHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor,
                IFileService documentService) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
                _documentService = documentService;
            }

            public async Task<Response<bool>> Handle(UpdateSeatsTypes request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.SeatsTypeRepository.GetById(request._updateSeatTypesDto.Id);
                var result ="";
                if (request._updateSeatTypesDto.SeatImage != null)
                {
                    result = await _documentService.UploadFile(new Contracts.Dtos.UploadFileModel
                    {
                        FormFile = request._updateSeatTypesDto.SeatImage,
                        FolderPrefix = Contracts.Enums.FolderType.ChairTypes
                    });
                }
                if(obj is not null)
                obj.UpdateSeatsType(request._updateSeatTypesDto.Name,
                    request._updateSeatTypesDto.Color,
                    request._updateSeatTypesDto.ColorText, 
                    result == "" ? obj.ImagePath : result);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateSeatsTypeDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
            public string ColorText { get; set; }
            public IFormFile? SeatImage { get; set; }
        }
    }
}
