using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagement.Api.Controllers.V1
{
    //[ApiVersion(1, Deprecated = true)]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeveloperController : BaseController
    {

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
            throw new NotImplementedException();
        }
    }
}
