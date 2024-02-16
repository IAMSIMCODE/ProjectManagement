using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement.Domain.Utility;

namespace ProjectManagement.Api.Controllers.V1
{
    //[ApiVersion(1, Deprecated = true)]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeveloperController : BaseController
    {
        public DeveloperController(ILoggerManager logger) : base(logger)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
            return Ok("Getting all Developers");
            //_logger.LogInfo(JsonConvert.ToString("Hello Testing"));
            //var cls = new TestClass()
            //{
            //    Id = 1,
            //    Name = "hello",
            //};

            //_logger.LogInfo(JsonConvert.SerializeObject(cls));
            throw new NotImplementedException();
        }
    }

    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}
