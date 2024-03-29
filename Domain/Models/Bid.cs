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
        public User BidId { get; set; }
        public TypeOfActivity TypeOfActivity { get; set; }

        [MaxLength(100)]
        public string Title {  get; set; }

        [MaxLength (300)]
        public string? ShortDescription {  get; set; }

        [MaxLength (1000)]
        public string Plan { get; set; }

    }
}
