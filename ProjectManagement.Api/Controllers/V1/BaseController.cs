using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Utility;

namespace ProjectManagement.Api.Controllers.V1
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ILoggerManager _logger;

        public BaseController(ILoggerManager logger)
        {
            _logger = logger;
        }
    }
}
