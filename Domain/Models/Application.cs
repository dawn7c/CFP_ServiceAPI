using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CfpService.Domain.Models
{
    public class Application 
    {
        
        public Application()
        {
        }

        public Application(Activity activity, string name, string? description, string outline)
        {
            
            Author = Guid.NewGuid();
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

        [JsonIgnore]
        public DateTime CreateDateTime { get; private set; }
        [JsonIgnore]
        public bool IsSend { get;   set; }
        [JsonIgnore]
        public DateTime? SendDateTime {  get;   set; }
    }
}
