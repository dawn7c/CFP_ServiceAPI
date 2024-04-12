
namespace Domain.Abstractions
{
    public interface IApplication
    {
        Task CreateBidAsync(T entity);
        Task<bool> Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetBidAfterDate(DateTime date);
        Task<List<T>> GetNotSubAfterDate(DateTime date);
        Task<T> GetBidId(Guid id);
        Task<T> GetNotSendedBidByUser(Guid id);
        Task<bool> CheckUnSendedBid(Guid id);
    }
}
