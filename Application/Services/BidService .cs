using Domain.Models;


namespace Application.Services
{
    public class BidService
    {
       
        public async Task<List<Bid>> GetDate(List<Bid> bids, DateTime time)
        {
            return  bids.Where(b => b.SendDateTime > time).ToList();
        }
    }
}
