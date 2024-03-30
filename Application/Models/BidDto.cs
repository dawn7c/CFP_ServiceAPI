using Domain;
using Domain.Abstractions;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class BidDto
    {
        //public BidDto(Author bidId, TypeOfActivity activity, string name, string? description, string outline)
        //{
        //    BidId = bidId;
        //    Activity = activity;
        //    Name = name;
        //    Description = description;
        //    Outline = outline;
        //}

        public Author BidId { get; set; }
        public TypeOfActivity Activity { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [MaxLength(1000)]
        public string Outline { get; set; }

    }
}
