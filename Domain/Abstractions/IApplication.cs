
using CfpService.Domain.Models;
using Application = CfpService.Domain.Models.Application;

namespace Domain.Abstractions
{
    public interface IApplication
    {
        Task CreateApplicationAsync(Application entity);
        Task<bool> UpdateDataAsync(Application entity);
        Task Delete(Application entity);
        Task<List<Application>> GetBidAfterDate(DateTime date);
        Task<List<Application>> GetNotSubAfterDate(DateTime date);
        Task<Application> GetApplicationId(Guid id);
        Task<Application> GetNotSendedApplicationByUser(Guid id);
        Task<bool> CheckUnSendedApplication(Guid id);
       
    }
}
