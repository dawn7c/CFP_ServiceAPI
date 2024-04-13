using Domain.Models;

namespace CfpService.Api.Models
{
    public class ApplicationUpdateRequest
    {
        public Activity Activity { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Outline { get; set; }
    }
}
