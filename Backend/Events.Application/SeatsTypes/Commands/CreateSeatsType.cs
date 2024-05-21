

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
    public class CreateSeatsType : IRequest<Response<bool>>
    {
        private readonly CreateSeatsTypeDto _createSeatsTypeDto;
        public CreateSeatsType(CreateSeatsTypeDto createSeatsTypeDto)
        {

            _createSeatsTypeDto = createSeatsTypeDto;

        }
        public class CreateSeatsTypeHandler : BaseHandler, IRequestHandler<CreateSeatsType, Response<bool>>
        {
            private readonly IFileService _documentService;
            public CreateSeatsTypeHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor,
                IFileService documentService) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
                _documentService = documentService;
            }

            public async Task<Response<bool>> Handle(CreateSeatsType request, CancellationToken cancellationToken)
            {
                var result = "";
                if (request._createSeatsTypeDto.SeatImage != null)
                {
                     result = await _documentService.UploadFile(new Contracts.Dtos.UploadFileModel
                    {
                        FormFile = request._createSeatsTypeDto.SeatImage,
                        FolderPrefix = Contracts.Enums.FolderType.ChairTypes
                    });
                }
              
                var chairType = new SeatsType(
                      request._createSeatsTypeDto.Name,
                      request._createSeatsTypeDto.Color,
                      request._createSeatsTypeDto.ColorText,
                      result == ""? null : result);
                _unitOfWork.SeatsTypeRepository.Insert(chairType);

                return await _unitOfWork.CommitAsync();
            }
        }
        public class CreateSeatsTypeDto
        {
            public string Name { get; set; }
            public string Color { get; set; }
            public string ColorText { get; set; }
            public IFormFile? SeatImage { get; set; }
        }
    }
}
