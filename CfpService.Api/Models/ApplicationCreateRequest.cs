using Domain.Models;

namespace Web.Models
{
    public class ApplicationCreateRequest
    {
        public Guid Author { get;  set; }
        public Activity Activity { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Outline { get; set; }
    }
}
