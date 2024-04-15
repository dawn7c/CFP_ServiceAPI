using AutoMapper;
using CfpService.Api.Models;

namespace CfpService.Api.Mapping
{
    public class ApplicationMappingService
    {
        private readonly IMapper _mapper;

        public ApplicationMappingService()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CfpService.Domain.Models.Application, ApplicationResponse>();
                cfg.CreateMap<CfpService.Domain.Models.Application, ApplicationCreateRequest>();
            });
            _mapper = configuration.CreateMapper();
        }

        public ApplicationResponse MapToApplicationResponse(Domain.Models.Application application)
        {
            return _mapper.Map<ApplicationResponse>(application);
        }

        public ApplicationCreateRequest MapToApplicationRequest(Domain.Models.Application application)
        {
            return _mapper.Map<ApplicationCreateRequest>(application);
        }
    }
}
