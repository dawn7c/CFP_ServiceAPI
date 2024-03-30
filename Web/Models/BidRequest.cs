using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BidRequest
    {
        public Guid Author { get; set; }
        public TypeOfActivity Activity { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [MaxLength(1000)]
        public string Outline { get; set; }
    }
}
