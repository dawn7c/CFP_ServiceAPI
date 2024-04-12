using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CfpService.Domain.Models
{
    public class Application 
    {
        
        public Application()
        {
        }

        public Application(Guid author, int activityId, string name, string? description, string outline)
        {
            Author = author;
            ActivityId = activityId;
            Name = name;
            Description = description;
            Outline = outline;
            CreateDateTime = DateTime.UtcNow;
        }

        public readonly Guid BaseEntity;
        public Guid Author { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }


        [MaxLength(100)]
        public string Name {  get; set; }

        [MaxLength (300)]
        public string? Description {  get; set; }

        [MaxLength (1000)]
        public string Outline { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsSend { get; set; }
        public DateTime? SendDateTime {  get; set; }

        
    }
}
