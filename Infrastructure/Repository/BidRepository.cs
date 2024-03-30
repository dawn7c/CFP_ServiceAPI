using Domain;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
   public class BidRepository<T> : IRepository<T> where T : BaseEntity
   {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public BidRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        private bool IsExist(Guid id, out T entity)
        {
            entity = _dbSet.Find(id);
            return entity != null;
        }

        public async Task CreateBidAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false); ;
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
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

        public Task<T> Send()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(T entity)
        {
            if (!IsExist(entity.Id, out _))
            {
                return false;
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
