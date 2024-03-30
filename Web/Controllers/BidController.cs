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

        public BidController(ApplicationContext context, IRepository<Bid> bidRepository)
        {
            _context = context;
            _bidRepository = bidRepository;
        }

        [HttpGet("bids/id")]
        public async Task<ActionResult<BidResponse>> GetBidAsync(Guid id)
        {
            
            var bid = await _bidRepository.GetBidId(id);
            if (bid == null)
            {
                return NotFound("Заявка не найдена");
            }
            return Ok(bid);
        }

        [HttpPost("Create bid")]
        public async Task<IActionResult> BidAddAsync(BidRequest bidRequest)
        {
            
            var bid = new Bid(bidRequest.Activity, bidRequest.Name, bidRequest.Description, bidRequest.Outline);
            await _bidRepository.CreateBidAsync(bid);
            return Ok(bid);
        }
        [HttpPost("Send bid")]
        public async Task<IActionResult> SendBidAsync(Guid bidId)
        {
            var bid = await _bidRepository.GetBidId(bidId);
            if (bid == null)
            {
                return NotFound("Заявка не найдена"); 
            }

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBid(Guid id)
        {
            var bid = await _bidRepository.GetBidId(id);
            if (bid is null)
                return NotFound("Заявка не найдена");

            await _bidRepository.Delete(bid);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Guid id, [FromBody] BidRequest request)
        {

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
