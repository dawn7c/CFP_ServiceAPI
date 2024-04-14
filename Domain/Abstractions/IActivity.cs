
using Domain.Models;

namespace Domain.Abstractions
{
    public interface IActivity
    {
        Task<List<object>> ListOfActivityWithDescriptionAsync();
        string ActivityDescription(Activity activity);
    }
}
