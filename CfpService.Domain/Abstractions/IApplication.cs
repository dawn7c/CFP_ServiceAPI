using CfpService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfpService.Domain.Abstractions
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
