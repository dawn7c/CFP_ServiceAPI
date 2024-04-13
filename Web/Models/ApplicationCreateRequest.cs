
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ApplicationCreateRequest
    {
        
        public Guid Author { get;  set; }
        public Activity Activity { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Outline { get; set; }

        public ApplicationCreateRequest(Guid author, Activity activity, string name, string? description, string outline)
        {
            Author = author;
            Activity = activity;
            Name = name;
            Description = description;
            Outline = outline;
        }
    }
}
