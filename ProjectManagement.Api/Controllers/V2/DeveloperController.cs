using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Controllers.V2
{
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeveloperController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
            return Ok("See Developers") ;
        }
    }
}
