using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
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
        public Bid()
        {
        }

        public Bid(TypeOfActivity activity, string name, string? description, string outline)
        {
            Author = Guid.NewGuid();
            Activity = activity;
            Name = name;
            Description = description;
            Outline = outline;
        }


        public Guid Author { get; set; }
        public TypeOfActivity Activity { get; set; }

        [MaxLength(100)]
        public string Name {  get; set; }

        [MaxLength (300)]
        public string? Description {  get; set; }

        [MaxLength (1000)]
        public string Outline { get; set; }
        
    }
}
