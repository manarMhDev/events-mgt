

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Services;
using Events.Contracts.Services.Interfaces;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Events.Application.EventPlaces.Commands.CreatePlace
{
    public class CreateEventPlace : IRequest<Response<bool>>
    {
        private readonly EventPlaceCreate _eventPlaceCreate;
        public CreateEventPlace(EventPlaceCreate eventPlaceCreate)
        {
            _eventPlaceCreate = eventPlaceCreate;
        }
        public class CreateEventPlaceHandler : BaseHandler, IRequestHandler<CreateEventPlace, Response<bool>>
        {
            private readonly IFileService _documentService;
            public CreateEventPlaceHandler(IUnitOfWork iUnitOfWork,
                IJwtTokenGenerator jwtTokenGenerator, 
                UserManager<ApplicationUser> userManager,
                IHttpContextAccessor httpContextAccessor,
                IFileService documentService) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
                _documentService = documentService;
            }

            public async Task<Response<bool>> Handle(CreateEventPlace request, CancellationToken cancellationToken)
            {
                var result = "";
                if (request._eventPlaceCreate.SeatingChartImage != null)
                {
                    result = await _documentService.UploadFile(new Contracts.Dtos.UploadFileModel
                    {
                        FormFile = request._eventPlaceCreate.SeatingChartImage,
                        FolderPrefix = Contracts.Enums.FolderType.EventPlaces
                    });
                }
              
                var eventPlace = new EventPlace(
                      request._eventPlaceCreate.Name,
                      request._eventPlaceCreate.SeatingChart,
                      result == "" ? null : result,
                      request._eventPlaceCreate.Columns,
                      request._eventPlaceCreate.Rows);
                _unitOfWork.EventPlaceRepository.Insert(eventPlace);

                return await _unitOfWork.CommitAsync();
                
            }
        }
        public class EventPlaceCreate
        {
            public string Name { get; set; }
            public Language Language { get; set; }
            public SeatingChart SeatingChart { get;  set; }
            public IFormFile? SeatingChartImage { get;  set; }
            public int? Columns { get;  set; }
            public int? Rows { get;  set; }
        }
    }
}
