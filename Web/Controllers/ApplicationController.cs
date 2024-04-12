using CfpService.Domain.Models;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IApplication _applicationRepository;
        private readonly ILogger<ApplicationController> _logger;
        private readonly IActivity _activityRepository;

        public ApplicationController(ApplicationContext context, IApplication applicationRepository, IActivity activityRepository, ILogger<ApplicationController> logger)
        {
            _context = context;
            _applicationRepository = applicationRepository;
            _logger = logger;
            _activityRepository = activityRepository;
        }

        [HttpGet("application/id")]
        public async Task<ActionResult<ApplicationResponse>> GetApplicationAsync(Guid id)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.GetApplicationId(id);
            if (bid == null)
            {
                return NotFound("Заявка не найдена");
            }
            return Ok(bid);
        }
        [HttpGet("applications")]
        public async Task<ActionResult<ApplicationResponse>> GetBidAfterDateAsync([FromQuery] DateTime submittedAfter)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.GetBidAfterDate(submittedAfter);
            return Ok(bid);
        }

        [HttpGet("application")]
        public async Task<ActionResult<ApplicationResponse>> GetBidNotSubmittionAndOlderDate(DateTime unsubmittedOlder)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.GetNotSubAfterDate(unsubmittedOlder);
            return Ok(bid);

        }
        [HttpGet("users/{id}/currentapplication")]
        public async Task<ActionResult<ApplicationResponse>> GetApplicationByUser(Guid id)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.GetNotSendedApplicationByUser(id);
            if (bid == null)
            {
                return BadRequest("Заявки по данному пользователю не найдена ");

            }
            return Ok(bid);

        }
        //[HttpPost("applications")]
        //public async Task<IActionResult> ApplicationAddAsync(ApplicationRequest bidRequest)
        //{
        //    _logger.LogInformation("Post request received");
        //    var check = await _applicationRepository.CheckUnSendedApplication(bidRequest.Author);
        //    if (check)
        //    {

        //        var activity = await _activityRepository.GetActivityByName(bidRequest.Activity);
        //        if (activity is null)
        //        {
        //            return BadRequest("Не найдена данная активность");
        //        }
        //        var bid = new Application(bidRequest.Author, activity.Id, bidRequest.Name, bidRequest.Description, bidRequest.Outline);
        //        await _bidRepository.CreateBidAsync(bid);
        //        return Ok(bid);
        //    }
        //    else
        //    {
        //        return BadRequest("У данного пользователя уже есть незакрытая заявка");
        //    }
        //}
        //[HttpPost("applications/{bidId}/submit")]
        //public async Task<IActionResult> SendApplicationAsync(Guid bidId)
        //{
        //    _logger.LogInformation("Post request received");
        //    var bid = await _applicationRepository.GetApplicationId(bidId);
        //    if (bid.IsSend == true)
        //    {
        //        return NotFound("Заявка была отправлена ранее");
        //    }
        //    else
        //    {
        //        bid.IsSend = true;
        //        bid.SendDateTime = DateTime.UtcNow;
        //        await _applicationRepository.Update(bid);
        //    }

        //    return Ok();
        //}

        [HttpDelete("applications/{id}")]
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            _logger.LogInformation("Delete request received");
            var bid = await _applicationRepository.GetApplicationId(id);

            if (bid is null)
                return NotFound("Заявка не найдена");
            if (bid.IsSend)
            {
                return BadRequest("Нельзя удалить отправленную заявку");

            }
            await _applicationRepository.Delete(bid);
            return Ok();
        }

        //[HttpPut("applications/{id}")]
        //public async Task<IActionResult> Update(Guid id, [FromBody] ApplicationRequest request)
        //{
        //    _logger.LogInformation("Put request received");
        //    var bid = await _applicationRepository.GetApplicationId(id);

        //    if (bid is null)
        //        return NotFound("Заявка не найдена");
        //    if (bid.IsSend)
        //    {
        //        return BadRequest("Нельзя обновить отправленную заявку");

        //    }
        //    bid.ActivityId = (await _activityRepository.GetActivityByName(request.Activity)).Id;
        //    bid.Name = request.Name;
        //    bid.Description = request.Description;
        //    bid.Outline = request.Outline;

        //    await _bidRepository.Update(bid);

        //    return Ok();
        //}
        //[HttpGet("activities")]
        //public async Task<ActionResult<List<ActivityResponse>>> GetAllActivities()
        //{
        //    _logger.LogInformation("GET request received");
        //    var activites = await _activityRepository.GetListOfActivity();
        //    if (activites == null)
        //    {
        //        return NotFound("Активность не найдена");
        //    }
        //    return Ok(activites);
        //}


    }
}
