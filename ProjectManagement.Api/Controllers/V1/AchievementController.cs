using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Domain.Models.DTO.Request;
using ProjectManagement.Domain.Services.Interfaces;
using ProjectManagement.Domain.Utility;

namespace ProjectManagement.Api.Controllers.V1
{
    //[ApiVersion(1, Deprecated = true)]
    [ApiVersion(1)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AchievementController : BaseController
    {
        private readonly IAchievementService _achievementService;
        public AchievementController(ILoggerManager logger, IAchievementService achievementService) : base(logger)
        {
            _achievementService = achievementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
            var achievements = await _achievementService.GetAll();
            if (achievements == null) { return NotFound(); }
            return Ok(achievements);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAchievement([FromBody] CreateDevAchievementRequest achievementRequest)
        {
            if (!ModelState.IsValid) { return BadRequest("Not a valid request"); }

            var result = await _achievementService.InsertAsync(achievementRequest);
            if (result == null) { return BadRequest("An Error Occured. Developer Achievement Could not be created"); }

            return Ok(result);
        }

        [HttpGet]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> GetDeveloperAndAchievements(Guid developerId)
        {
            var achievement = await _achievementService.GetById(developerId);

            if (achievement == null) { return NotFound("Developer not found"); }

            return Ok(achievement);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeveloper([FromBody] UpdateDevAchievementRequest updateAchievement)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }
            var result = await _achievementService.UpdateAsync(updateAchievement);

            if (!result) { return BadRequest("Could not update entity"); }
            return NoContent();
        }

        [HttpDelete]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> DeleteDeveloper(Guid developerId)
        {
            var result = await _achievementService.DeleteAsync(developerId);

            if (!result) { return BadRequest("Could not delete entity"); }
            return NoContent();
        }
    }
}
