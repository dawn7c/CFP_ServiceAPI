
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
   public class ApplicationRepository<T> : IApplication<T> where T : Bid
   {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;

        public ApplicationRepository(ApplicationContext context)
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


        public async Task<List<T>> GetBidAfterDate(DateTime date)
        {
            return await _dbSet.Where(b => b.SendDateTime > date.ToUniversalTime()).ToListAsync();
        }
        public async Task<List<T>> GetNotSubAfterDate(DateTime date)
        {
            return await _dbSet.Where(b => b.CreateDateTime > date.ToUniversalTime() && !b.IsSend).ToListAsync();
        }
        public async Task<T> GetNotSendedBidByUser(Guid id)
        {
            return await _dbSet.Where(b => b.Author == id && !b.IsSend).FirstOrDefaultAsync();
        }
        public async Task<bool> CheckUnSendedBid(Guid id)
        {
             var bids = await _dbSet.Where(b => b.Author == id && !b.IsSend).ToListAsync();
             return bids.Count() > 1 ? false : true;
        }
        public async Task<T> GetBidId(Guid id)
        {
            return await _dbSet.FindAsync(id);
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
