using CfpService.Api.Models;
using CfpService.Domain.Models;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Web.Models;

namespace Web.Controllers
{

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

        [HttpGet("application/{id}")]
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
        public async Task<ActionResult<ApplicationResponse>> GetBidNotSubmittionAndOlderDateAsync(DateTime unsubmittedOlder)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.GetNotSubAfterDate(unsubmittedOlder);
            return Ok(bid);

        }
        [HttpGet("users/{id}/currentapplication")]
        public async Task<ActionResult<ApplicationResponse>> GetApplicationByUserAsync(Guid id)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.GetNotSendedApplicationByUser(id);
            if (bid == null)
            {
                return BadRequest("Заявки по данному пользователю не найдена ");

            }
            return Ok(bid);

        }

        [HttpPost("applications")]
        public async Task<IActionResult> ApplicationAddAsync(ApplicationCreateRequest applicationRequest)
        {
            _logger.LogInformation("Post request received");

            //// Проверяем, существует ли активность с указанным именем
            //if (!Enum.TryParse(typeof(Activity),applicationRequest.Activity.ToString(), true, out object activityEnum))
            //{
            //    return BadRequest("Указанная активность не найдена");
            //}

            //// Проверяем, существует ли активность с указанным именем в enum
            //if (!Enum.IsDefined(typeof(Activity), activityEnum))
            //{
            //    return BadRequest("Указанная активность не найдена");
            //}

            // Получаем описание активности
            //var activityDescription = await _activityRepository.GetActivityDescription(activityEnum);

            // Проверяем, есть ли незакрытая заявка у указанного пользователя
            var check = await _applicationRepository.CheckUnSendedApplication(applicationRequest.Author);
            if (check)
            {
                // Создаем новую заявку
                var bid = new Application(applicationRequest.Activity, applicationRequest.Name, applicationRequest.Description, applicationRequest.Outline);
                await _applicationRepository.CreateApplicationAsync(bid);
                return Ok(bid);
            }
            else
            {
                return BadRequest("У данного пользователя уже есть незакрытая заявка");
            }
        }

        [HttpPost("applications/{id}/submit")]
        public async Task<IActionResult> SendApplicationAsync(Guid id)
        {
            _logger.LogInformation("Post request received");
            var bid = await _applicationRepository.GetApplicationId(id);
            if (bid.IsSend == true)
            {
                return NotFound("Заявка была отправлена ранее");
            }
            else
            {
                bid.IsSend = true;
                bid.SendDateTime = DateTime.UtcNow;
                await _applicationRepository.UpdateDataAsync(bid);
            }

            return Ok("OK,200");
        }

        [HttpDelete("applications/{id}")]
        public async Task<IActionResult> DeleteApplicationAsync(Guid id)
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
            return Ok("OK,200");
        }

        [HttpPut("applications/{id}")]
        public async Task<IActionResult> UpdateApplicationAsync(Guid id, [FromBody] ApplicationUpdateRequest request)
        {
            _logger.LogInformation("Put request received");
            var application = await _applicationRepository.GetApplicationId(id);

            if (application == null)
                return NotFound("Заявка не найдена");

            if (application.IsSend == true)
            {
                return BadRequest("Нельзя обновить отправленную заявку");
            }

            var activity = _activityRepository.GetActivityDescription(request.Activity);

            if (activity == null)
            {
                return BadRequest("Указанной активности не существует");
            }

            application.Activity = request.Activity;
            application.Name = request.Name;
            application.Description = request.Description;
            application.Outline = request.Outline;
             
            await _applicationRepository.UpdateDataAsync(application);

            return Ok(new
            {
                id = application.Id,
                author = application.Author,
                activity = application.Activity,
                name = application.Name,
                description = application.Description,
                outline = application.Outline
            });
        }


        [HttpGet("activities")]
        public async Task<ActionResult<List<ActivityResponse>>> GetAllActivitiesAsync()
        {
            _logger.LogInformation("GET request received");
            var activites = await _activityRepository.GetListOfActivityWithDescription();
            if (activites == null)
            {
                return NotFound("Активность не найдена");
            }
            return Ok(activites);
        }
    }
}
