using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfpService.Application.Models
{
    public class ApplicationDto
    {
        public Guid Author { get; init; }
        public int ActivityId { get; init; }
        public Activity Activity { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
        public string Outline { get; init; }
        public DateTime CreateDateTime { get; init; }
        public bool IsSend { get; init; }
        public DateTime? SendDateTime { get; init; }
    }
}
