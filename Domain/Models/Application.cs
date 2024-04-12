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
        public Guid Author { get; init; }
        public int ActivityId { get; init; }
        public Activity Activity { get; init; }
        public string Name {  get; init; }
        public string? Description {  get; init; }
        public string Outline { get; init; }
        public DateTime CreateDateTime { get; init; }
        public bool IsSend { get; init; }
        public DateTime? SendDateTime {  get; init; }

        
    }
}
