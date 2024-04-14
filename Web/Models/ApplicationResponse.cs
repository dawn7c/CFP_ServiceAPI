namespace Web.Models
{
    public class ApplicationResponse
    {
        public Guid Id {  get; set; }
        public Guid Author { get; private set; }
        public string Activity { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Outline { get; set; }
    }
}
