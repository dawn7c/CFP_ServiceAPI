using AutoMapper;
using CfpService.Api.Models;
using CfpService.Application.Validation;
using CfpService.Domain.Models;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Web.Models;
using static System.Net.Mime.MediaTypeNames;

namespace Web.Controllers
{

    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IApplication _applicationRepository;
        private readonly ILogger<ApplicationController> _logger;
        private readonly IActivity _activityRepository;
        private readonly ApplicationValidator _validator;


        public ApplicationController(ApplicationContext context, IApplication applicationRepository, IActivity activityRepository, ILogger<ApplicationController> logger)
        {
            _context = context;
            _applicationRepository = applicationRepository;
            _logger = logger;
            _activityRepository = activityRepository;
            _validator = new ApplicationValidator();
            
        }

        [HttpGet("application/{id}")]
        public async Task<ActionResult<ApplicationResponse>> GetApplicationByIdAsync(Guid id)
        {
            _logger.LogInformation("GET request received");
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application); 
            if (!validationResult.IsValid)
            {
                return NotFound(validationResult.ErrorMessage);
            }
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CfpService.Domain.Models.Application, ApplicationResponse>();
            });
            var mapper = configuration.CreateMapper();
            var res = mapper.Map<ApplicationResponse>(application);
            return Ok(res);
        }

        [HttpGet("applications")]
        public async Task<ActionResult<ApplicationResponse>> GetApplicationsSubmittedAfterAsync([FromQuery] DateTime submittedAfter)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.ApplicationsSubmittedAfterAsync(submittedAfter);
            return Ok(bid);
        }

        [HttpGet("application")]
        public async Task<ActionResult<ApplicationResponse>> GetBidNotSubmittionAndOlderDateAsync(DateTime unsubmittedOlder)
        {
            _logger.LogInformation("GET request received");
            var bid = await _applicationRepository.UnsubmittedApplicationsOlderThanDate(unsubmittedOlder);
            return Ok(bid);

        }

        [HttpGet("users/{id}/currentapplication")]
        public async Task<ActionResult<ApplicationResponse>> GetCurrentUnsubmittedApplicationByUserAsync(Guid id)
        {
            _logger.LogInformation("GET request received");
            var application = await _applicationRepository.CurrentUnsubmittedApplicationByUserAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);
            
            return Ok(application);

        }

        [HttpPost("applications")]
        public async Task<IActionResult> PostApplicationAddAsync(ApplicationCreateRequest applicationRequest)
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
            var check = await _applicationRepository.HasUserSubmittedApplicationAsync(applicationRequest.Author);
            if (check)
            {
                // Создаем новую заявку
                var bid = new CfpService.Domain.Models.Application(applicationRequest.Activity, applicationRequest.Name, applicationRequest.Description, applicationRequest.Outline);
                await _applicationRepository.CreateApplicationAsync(bid);
                var configuration = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CfpService.Domain.Models.Application, ApplicationResponse>();
                });
                var mapper = configuration.CreateMapper();
                var res = mapper.Map<ApplicationResponse>(bid);
                return Ok(res);
            }
            else
            {
                return BadRequest("У данного пользователя уже есть незакрытая заявка");
            }
        }

        [HttpPost("applications/{id}/submit")]
        public async Task<IActionResult> PostApplicationAsync(Guid id)
        {
            _logger.LogInformation("Post request received");
            var bid = await _applicationRepository.ApplicationByIdAsync(id);
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
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);

            if (application.IsSend == true)
            {
                return BadRequest("Нельзя удалить отправленную заявку");

            }
            await _applicationRepository.Delete(application);
            return Ok("OK,200");
        }

        [HttpPut("applications/{id}")]
        public async Task<IActionResult> UpdateApplicationAsync(Guid id, [FromBody] ApplicationUpdateRequest request)
        {
            _logger.LogInformation("Put request received");
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);

            if (application.IsSend == true)
            {
                return BadRequest("Нельзя обновить отправленную заявку");
            }

            var activity = _activityRepository.ActivityDescription(request.Activity);
            // validationResult = _validator.CheckForNullActivity(activity);
            //if (!validationResult.IsValid)
            //    return BadRequest(validationResult.ErrorMessage);

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
            var activites = await _activityRepository.ListOfActivityWithDescriptionAsync();
            if (activites == null)
            {
                return NotFound("Активность не найдена");
            }
            return Ok(activites);
        }
    }
}
