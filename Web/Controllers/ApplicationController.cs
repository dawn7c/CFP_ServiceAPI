using CfpService.Api.Mapping;
using CfpService.Api.Models;
using CfpService.Application.Validation;
using Domain.Abstractions;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Web.Models;


namespace Web.Controllers
{

    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IApplication _applicationRepository;
        private readonly ApplicationMappingService _mappingService;
        private readonly IActivity _activityRepository;
        private readonly ApplicationValidator _validator;
        
        public ApplicationController(ApplicationContext context, IApplication applicationRepository, IActivity activityRepository)
        {
            _context = context;
            _applicationRepository = applicationRepository;
            _activityRepository = activityRepository;
            _validator = new ApplicationValidator();
            _mappingService = new ApplicationMappingService();
        }

        [HttpGet("application/{id}")]
        public async Task<ActionResult<ApplicationResponse>> GetApplicationByIdAsync(Guid id)
        {
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application); 
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);
            var res = _mappingService.MapToApplicationResponse(application);
            return Ok(res);
        }

        [HttpGet("applications/submitted")]
        public async Task<ActionResult<List<ApplicationCreateRequest>>> GetApplicationsSubmittedAfterAsync([FromQuery] DateTime submittedAfter)
        {
            var applications = await _applicationRepository.ApplicationsSubmittedAfterAsync(submittedAfter);
            var res = applications.Select(application => _mappingService.MapToApplicationResponse(application)).ToList();
            return Ok(res);
        }

        [HttpGet("applications/unsubmitted")]
        public async Task<ActionResult<List<ApplicationCreateRequest>>> GetApplictionsNotSubmittionAndOlderDateAsync([FromQuery]DateTime unsubmittedOlder)
        {
            var applications = await _applicationRepository.UnsubmittedApplicationsOlderThanDate(unsubmittedOlder);
            var res = applications.Select(application => _mappingService.MapToApplicationResponse(application)).ToList();
            return Ok(res);
        }

        [HttpGet("users/{id}/current-application")]
        public async Task<ActionResult<ApplicationCreateRequest>> GetCurrentUnsubmittedApplicationByUserAsync(Guid id)
        {
            var application = await _applicationRepository.CurrentUnsubmittedApplicationByUserAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);
            var res = _mappingService.MapToApplicationResponse(application);
            return Ok(res);
        }

        [HttpPost("applications")]
        public async Task<IActionResult> PostApplicationAddAsync(ApplicationCreateRequest applicationRequest)
        {
            var validationResult = _validator.ValidateAuthorId(applicationRequest.Author);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);
            var check = await _applicationRepository.HasUserSubmittedApplicationAsync(applicationRequest.Author);
             validationResult = _validator.CheckForNullForCreateApplication(check);
            if (validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);
                var bid = new CfpService.Domain.Models.Application(applicationRequest.Author, applicationRequest.Activity, applicationRequest.Name, applicationRequest.Description, applicationRequest.Outline);
                await _applicationRepository.CreateApplicationAsync(bid);
                var res = _mappingService.MapToApplicationResponse(bid);
                return Ok(res);
        }

        [HttpPost("applications/{id}/submit")]
        public async Task<IActionResult> PostApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);
            validationResult = _validator.ValidateRequiredFields(application);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);
            validationResult = _validator.UpdateApplicationSendStatusAsync(application);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);
            await _applicationRepository.UpdateDataAsync(application);
            return Ok("OK,200");
        }

        [HttpDelete("applications/{id}")]
        public async Task<IActionResult> DeleteApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);
            validationResult = _validator.CheckForSend(application);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);
            await _applicationRepository.Delete(application);
            return Ok("OK,200");
        }

        [HttpPut("applications/{id}")]
        public async Task<IActionResult> UpdateApplicationAsync(Guid id, [FromBody] ApplicationUpdateRequest request)
        {
            var application = await _applicationRepository.ApplicationByIdAsync(id);
            var validationResult = _validator.CheckForNull(application);
            if (!validationResult.IsValid)
                return NotFound(validationResult.ErrorMessage);
            validationResult = _validator.CheckForSend(application);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ErrorMessage);
             _applicationRepository.UpdateApplicationProperties(application, request.Activity, request.Name, request.Description, request.Outline);
            await _applicationRepository.UpdateDataAsync(application);
            var applicationResponse = _mappingService.MapToApplicationResponse(application);
            return Ok(applicationResponse);
        }

        [HttpGet("activities")]
        public async Task<ActionResult<List<ActivityResponse>>> GetAllActivitiesAsync()
        {
            var activites = await _activityRepository.ListOfActivityWithDescriptionAsync();
            return Ok(activites);
        }
    }
}
