using CfpService.Domain.Models;
using CfpService.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using CfpService.Domain.Abstractions;


namespace CfpService.DataAccess.ApplicationRepository
{
   public class ApplicationRepository : IApplication
   {
        private readonly ApplicationContext _context;
        private readonly DbSet<CfpService.Domain.Models.Application> _dbSet;

        public ApplicationRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = _context.Set<CfpService.Domain.Models.Application>();
        }

        private async Task<bool> IsExist(CfpService.Domain.Models.Application entity)
        {
            bool exists = await _dbSet.AnyAsync(e => e.Id == entity.Id);
            return exists;
        }

        public async Task CreateApplicationAsync(CfpService.Domain.Models.Application entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(CfpService.Domain.Models.Application entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CfpService.Domain.Models.Application>> ApplicationsSubmittedAfterAsync(DateTime date)
        {
            return await _dbSet.Where(b => b.SendDateTime > date.ToUniversalTime()).ToListAsync();
        }

        public async Task<List<CfpService.Domain.Models.Application>> UnsubmittedApplicationsOlderThanDate(DateTime date)
        {
            return await _dbSet.Where(b => b.CreateDateTime > date.ToUniversalTime() && !b.IsSend).ToListAsync();
        }

        public async Task<CfpService.Domain.Models.Application?> CurrentUnsubmittedApplicationByUserAsync(Guid id)
        {
            return await _dbSet.Where(b => b.Author == id && !b.IsSend).FirstOrDefaultAsync();
        }

        public async Task<bool> HasUserSubmittedApplicationAsync(Guid id)
        {
             var bids = await _dbSet.Where(b => b.Author == id && !b.IsSend).ToListAsync();
             return bids.Count() > 0 ? false : true;
        }

        public async Task<CfpService.Domain.Models.Application> ApplicationByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> UpdateDataAsync(CfpService.Domain.Models.Application entity)
        {
            if (!await IsExist(entity))
            {
                return false;
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public void UpdateApplicationProperties(CfpService.Domain.Models.Application application, Activity activity, string name, string description, string outline)
        {
            application.Activity = activity;
            application.Name = name;
            application.Description = description;
            application.Outline = outline;
        }
   }
}
