using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Bid : BaseEntity
    {
        public Author BidId { get; set; }
        public TypeOfActivity Activity { get; set; }

        [MaxLength(100)]
        public string Name {  get; set; }

        [MaxLength (300)]
        public string? Description {  get; set; }

        [MaxLength (1000)]
        public string Outline { get; set; }

    }
}
