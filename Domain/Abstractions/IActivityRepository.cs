
namespace Domain.Abstractions
{
    public interface IActivityRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetListOfActivity();
        Task<T> GetActivityByName(string activityName);
    }
}
