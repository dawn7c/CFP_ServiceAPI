using Domain.Models;
using Application = CfpService.Domain.Models.Application;

namespace Domain.Abstractions
{
    public interface IApplication
    {
        Task CreateApplicationAsync(Application entity);
        Task<bool> UpdateDataAsync(Application entity);
        Task Delete(Application entity);
        Task<List<Application>> ApplicationsSubmittedAfterAsync(DateTime date);
        Task<List<Application>> UnsubmittedApplicationsOlderThanDate(DateTime date);
        Task<Application> ApplicationByIdAsync(Guid id);
        Task<Application> CurrentUnsubmittedApplicationByUserAsync(Guid id);
        Task<bool> HasUserSubmittedApplicationAsync(Guid id);
        void UpdateApplicationProperties(Application application, Activity activity, string name, string description, string outline);
    }
}
