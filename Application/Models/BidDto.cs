using Domain;
using Domain.Abstractions;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BidDto<T> : IRepository<T> where T : BaseEntity
    {
        public Task<T> CreateBid()
        {
            throw new NotImplementedException();
        }

        public Task<T> DeleteBid()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetBidAfterDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetBidId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetCurrentBid(Author id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetListOfActivity()
        {
            throw new NotImplementedException();
        }

        public Task<T> PostBid()
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateBid()
        {
            throw new NotImplementedException();
        }
    }
}
