using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Pagination;
using ProjectManagement.Domain.Services.Interfaces;
using ProjectManagement.Domain.Utility;

namespace ProjectManagement.Api.Controllers.V1
{
    //[ApiVersion(1, Deprecated = true)]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeveloperController : BaseController
    {
        private readonly IDeveloperService _developerService;
        public DeveloperController(ILoggerManager logger, IDeveloperService developerService) : base(logger)
        {
            _developerService = developerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
            var developers = await _developerService.GetAll();
            if (developers == null) { return NotFound(); }
            return Ok(developers);
        }

        [HttpGet("pageList")]
        public async Task<IActionResult> GetPageList([FromQuery] RequestParams requestParams)
        {
            var developers = await _developerService.GetPageList(requestParams);
            if (developers == null) { return NotFound(); }
            return Ok(developers);
        }

        [HttpGet]
        [Route("ApiDeveloper")]
        public async Task<IActionResult> GetApiDevelopers()
        {
            var developers = await _developerService.GetApiDevelopers();
            if (developers == null) { return NotFound(); }
            return Ok(developers);
        }

        [HttpGet]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> GetDeveloperAndAchievements(Guid developerId)
        {
            var developer = await _developerService.GetById(developerId);

            if (developer == null) { return NotFound("Developer not found"); }

            return Ok(developer);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeveloper([FromBody] CreateDeveloperRequest developerRequest)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }

            var result = await _developerService.InsertAsync(developerRequest);
            if (result == null) { return BadRequest("An Error Occured. Developer Could not be created"); }
            
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeveloper([FromBody] UpdateDeveloperRequest updateDeveloper)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }
            var result = await _developerService.UpdateAsync(updateDeveloper);

            if (!result) { return BadRequest("Could not update entity"); }
            return NoContent();
        }

        [HttpDelete]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> DeleteDeveloper(Guid developerId)
        {
            var result = await _developerService.DeleteAsync(developerId);

            if (!result) { return BadRequest("Could not delete entity"); }
            return NoContent();
        }
    }
}
