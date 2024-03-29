using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Author : BaseEntity
    {
        public Guid UserId {  get; set; }
        
    }
}
