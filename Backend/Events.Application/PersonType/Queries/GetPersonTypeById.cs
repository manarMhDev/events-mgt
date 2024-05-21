

using Events.Application.Common.Interfaces.Authentication;
using Events.Contracts.Wrappers;
using Events.Domain.Entities;
using Events.Domain.Enums;
using Events.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using static Events.Application.PersonType.Queries.GetPersonTypeById;

namespace Events.Application.PersonType.Queries
{
    public class GetPersonTypeById : IRequest<Response<PersonTypeDto>>
    {
        private readonly int _id;
        public GetPersonTypeById(int id)
        {

            _id = id;

        }
        public class GetPersonTypeByIdHandler : BaseHandler, IRequestHandler<GetPersonTypeById, Response<PersonTypeDto>>
        {
            public GetPersonTypeByIdHandler(IUnitOfWork iUnitOfWork, IJwtTokenGenerator jwtTokenGenerator, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : base(iUnitOfWork, jwtTokenGenerator, userManager, httpContextAccessor)
            {
            }

            public async Task<Response<PersonTypeDto>> Handle(GetPersonTypeById request, CancellationToken cancellationToken)
            {
                var obj = await _unitOfWork.PersonTypeRepository.GetById(request._id);
                var model = new PersonTypeDto();
                model.Id = obj.Id;
                model.Name = obj.Name;
                model.Color = obj.Color;
                
                return new Response<PersonTypeDto>(model);
            }
        }
        public class PersonTypeDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }
    }
}
