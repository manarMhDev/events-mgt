

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Events.Contracts.Services
{
    public class BaseService
    {
        protected readonly IConfiguration _configuration;
        protected readonly IWebHostEnvironment _webHostEnvironment;
        public BaseService(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
    }
}
