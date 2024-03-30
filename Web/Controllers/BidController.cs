using Application.Models;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore.Migrations;

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

        [HttpGet]
        public async Task<ActionResult<BidDto>> GetBid(Guid id)
        {
            var bid = await _bidRepository.GetBidId(id);
            return Ok(bid);
        }

        
    }
}
