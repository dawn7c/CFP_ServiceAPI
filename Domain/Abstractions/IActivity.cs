
namespace Domain.Abstractions
{
    public interface IActivity
    {
        Task<List>> IActivity();
        Task GetActivityByName(string activityName);
    }
}
