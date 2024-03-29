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
        Task<T> CreateBid();
        Task<T> UpdateBid();
        Task<T> DeleteBid();
        Task<T> PostBid();
        Task<T> GetBidAfterDate(DateTime date);
        Task<T> GetCurrentBid(Author id);
        Task<T> GetBidId(Guid id);

    }
}
