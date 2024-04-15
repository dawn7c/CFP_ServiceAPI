using CfpService.Domain.Models;

namespace CfpService.Domain.Models
{
    public class Application 
    {
        public Application()
        {
        }

        public Application(Guid author,Activity activity, string name, string? description, string outline)
        {
            Id = Guid.NewGuid();
            Author = author;
            Activity = activity;
            Name = name;
            Description = description;
            Outline = outline;
            CreateDateTime = DateTime.UtcNow;
        }

        public Guid Id { get; init; }
        public Guid Author { get; init; }
        public Activity Activity { get;  set; }
        public string Name {  get;  set; }
        public string? Description {  get;  set; }
        public string Outline { get;  set; }
        public DateTime CreateDateTime { get; private set; }
        public bool IsSend { get;   set; }
        public DateTime? SendDateTime {  get;   set; }
    }
}
