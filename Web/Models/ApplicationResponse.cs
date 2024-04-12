using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ApplicationResponse
    {
        public Guid Author { get; set; }
        public string Activity { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string? Description { get; set; }

        [MaxLength(1000)]
        public string Outline { get; set; }

        public ApplicationResponse(Guid author, string activity, string name, string? description, string outline)
        {
            Author = author;
            Activity = activity;
            Name = name;
            Description = description;
            Outline = outline;
        }
    }
}
