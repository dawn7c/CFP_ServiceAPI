using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface IActivityRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetListOfActivity();
        Task<T> GetActivityByName(string activityName);
    }
}
