

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.EventPlaces.Commands.Update
{
    public class UpdateEventPlace : IRequest<Response<bool>>
    {
        private readonly UpdateEventPlaceDto _updateEventPlaceDto;
        public UpdateEventPlace(UpdateEventPlaceDto updateEventPlaceDto)
        {

            _updateEventPlaceDto = updateEventPlaceDto;

        }
        public class UpdateEventPlaceHandler : BaseHandler, IRequestHandler<UpdateEventPlace, Response<bool>>
        {
            private readonly IFileService _documentService;
            public UpdateEventPlaceHandler(IUnitOfWork iUnitOfWork, 
                IJwtTokenGenerator jwtTokenGenerator,
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor,
                IFileService documentService) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
                _documentService = documentService;
            }

            public async Task<Response<bool>> Handle(UpdateEventPlace request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.EventPlaceRepository.GetById(request._updateEventPlaceDto.Id);
                var result = "";
                if (request._updateEventPlaceDto.SeatingChartImagePath != null)
                {
                    result = await _documentService.UploadFile(new Contracts.Dtos.UploadFileModel
                    {
                        FormFile = request._updateEventPlaceDto.SeatingChartImagePath,
                        FolderPrefix = Contracts.Enums.FolderType.ChairTypes
                    });
                }
                if (obj is not null)
                    obj.UpdateEventPlace(request._updateEventPlaceDto.Name, 
                        request._updateEventPlaceDto.SeatingChart,
                         result == "" ? obj.SeatingChartImagePath : result,
                        request._updateEventPlaceDto.Columns,
                        request._updateEventPlaceDto.Rows);
                return await _unitOfWork.CommitAsync();
            }
        }
        public class UpdateEventPlaceDto
        {
            public int Id { get; set; }
            public string Name { get;  set; } = null!;
            public SeatingChart SeatingChart { get;  set; }
            public IFormFile? SeatingChartImagePath { get;  set; }
            public int? Columns { get;  set; }
            public int? Rows { get;  set; }
        }
    }
}