
using CfpService.Domain.Models;
using Application = CfpService.Domain.Models.Application;

namespace Domain.Abstractions
{
    public interface IApplication
    {
        Task CreateBidAsync(Application entity);
        Task<bool> Update(Application entity);
        Task Delete(Application entity);
        Task<List<Application>> GetBidAfterDate(DateTime date);
        Task<List<Application>> GetNotSubAfterDate(DateTime date);
        Task<Application> GetBidId(Guid id);
        Task<Application> GetNotSendedBidByUser(Guid id);
        Task<bool> CheckUnSendedBid(Guid id);
    }
}
