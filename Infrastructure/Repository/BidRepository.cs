﻿using Domain;
using Domain.Abstractions;
using Domain.Models;
using Infrastructure.DatabaseContext;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
   public class BidRepository<T> : IRepository<T> where T : Bid
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


        public async Task<List<T>> GetBidAfterDate(DateTime date)
        {

            return await _dbSet.Where(b => b.SendDateTime > date.ToUniversalTime()).ToListAsync();
            
        }
        public async Task<List<T>> GetNotSubAfterDate(DateTime date)
        {

            return await _dbSet.Where(b => b.CreateDateTime > date.ToUniversalTime() && !b.IsSend).ToListAsync();

        }

        public async Task<T> GetBidId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        

        public async Task<T> GetListOfActivity()
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
