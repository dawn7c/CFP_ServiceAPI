
using CfpService.Domain.Models;
using Domain.Abstractions;
using Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Infrastructure.Repository
{
   public class ApplicationRepository : IApplication
   {
        private readonly ApplicationContext _context;
        private readonly DbSet<Application> _dbSet;

        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<Application>();
        }

        private async Task<bool> IsExist(Application entity)
        {
            bool exists = await _dbSet.AnyAsync(e => e.Id == entity.Id);
            return exists;
        }

        public async Task CreateApplicationAsync(Application entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Application entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task<List<Application>> ApplicationsSubmittedAfterAsync(DateTime date)
        {
            return await _dbSet.Where(b => b.SendDateTime > date.ToUniversalTime()).ToListAsync();
        }

        public async Task<List<Application>> UnsubmittedApplicationsOlderThanDate(DateTime date)
        {
            return await _dbSet.Where(b => b.CreateDateTime > date.ToUniversalTime() && !b.IsSend).ToListAsync();
        }

        public async Task<Application> CurrentUnsubmittedApplicationByUserAsync(Guid id)
        {
            return await _dbSet.Where(b => b.Author == id && !b.IsSend).FirstOrDefaultAsync();
        }

        public async Task<bool> HasUserSubmittedApplicationAsync(Guid id)
        {
             var bids = await _dbSet.Where(b => b.Author == id && !b.IsSend).ToListAsync();
             return bids.Count() > 1 ? false : true;
        }

        public async Task<Application> ApplicationByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateDataAsync(Application entity)
        {
            if (!await IsExist(entity))
            {
                return false;
            }
            //entity.UpdateApplication(entity.Name,entity.Description,entity.Outline);

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        
        

   }
}
