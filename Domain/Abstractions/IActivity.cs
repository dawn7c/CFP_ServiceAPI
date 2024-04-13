
using Domain.Models;

namespace Domain.Abstractions
{
    public interface IActivity
    {
        Task<List<object>> GetListOfActivityWithDescription();
        string GetActivityDescription(Activity activity);
    }
}
