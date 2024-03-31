using Application.Models;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Migrations;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IRepository<Bid> _bidRepository;
        private readonly ILogger<BidController> _logger;

        public BidController(ApplicationContext context, IRepository<Bid> bidRepository, ILogger<BidController> logger)
        {
            _context = context;
            _bidRepository = bidRepository;
            _logger = logger;
        }



        [HttpGet("bids/id")]
        public async Task<ActionResult<BidResponse>> GetBidAsync(Guid id)
        {
            _logger.LogInformation("GET request received");
            var bid = await _bidRepository.GetBidId(id);
            if (bid == null)
            {
                return NotFound("Заявка не найдена");
            }
            return Ok(bid);
        }
        [HttpGet("AfterDate")]
        public async Task<ActionResult<BidResponse>> GetBidAfterDateAsync(DateTime time)
        {
            _logger.LogInformation("GET request received");
            var bid = await _bidRepository.GetBidAfterDate(time);
            return Ok(bid);
        }

        [HttpGet("NotSubAndOlderDate")]
        public async Task<ActionResult<BidResponse>> GetBidNotSubmittionAndOlderDate(DateTime time)
        {
            _logger.LogInformation("GET request received");
            var bid = await _bidRepository.GetNotSubAfterDate(time);
            return Ok(bid);

        }

        [HttpPost("CreateBid")]
        public async Task<IActionResult> BidAddAsync(BidRequest bidRequest)
        {
            _logger.LogInformation("Post request received");
            var bid = new Bid(bidRequest.Activity, bidRequest.Name, bidRequest.Description, bidRequest.Outline);
            await _bidRepository.CreateBidAsync(bid);
            return Ok(bid);
        }
        [HttpPost("SendBid")]
        public async Task<IActionResult> SendBidAsync(Guid bidId)
        {
            _logger.LogInformation("Post request received");
            var bid = await _bidRepository.GetBidId(bidId);
            if (bid.IsSend == true)
            {
                return NotFound("Заявка была отправлена ранее");
            }
            else
            {
                bid.IsSend = true;
                bid.SendDateTime = DateTime.UtcNow;
                await _bidRepository.Update(bid);
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            _logger.LogInformation("Delete request received");
            var bid = await _bidRepository.GetBidId(id);
            if (bid is null)
                return NotFound("Заявка не найдена");

            await _bidRepository.Delete(bid);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] BidRequest request)
        {
            _logger.LogInformation("Put request received");
            var bid = await _bidRepository.GetBidId(id);

            if (bid is null)
                return NotFound("Заявка не найдена");

            bid.Activity = request.Activity;
            bid.Name = request.Name;
            bid.Description = request.Description;
            bid.Outline = request.Outline;

            await _bidRepository.Update(bid);

            return Ok();
        }

        

    }
}
