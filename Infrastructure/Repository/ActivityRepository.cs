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
    public class ActivityRepository : IActivity
    {

        //private readonly DbSet<T> _dbSet;

        //public ActivityRepository(ApplicationContext context)
        //{
        //    _context = context;
        //    _dbSet = _context.Set<T>();
        //}

        //public async Task<List<T>> GetListOfActivity()
        //{
        //    return await _dbSet.ToListAsync();

        //}
        //public async Task GetActivityByName(string name)
        //{
        //    return await _dbSet.Where(e=> e.Type == name).FirstOrDefaultAsync();
        //}
        public Task GetActivityByName(string activityName)
        {
            throw new NotImplementedException();
        }
    }
}
