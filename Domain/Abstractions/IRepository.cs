using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetListOfActivity();
        Task CreateBidAsync(T entity);
        Task<bool> Update(T entity);
        Task Delete(T entity);
        Task<List<T>> GetBidAfterDate(DateTime date);
        Task<List<T>> GetNotSubAfterDate(DateTime date);
        Task<T> GetBidId(Guid id);

    }
}
