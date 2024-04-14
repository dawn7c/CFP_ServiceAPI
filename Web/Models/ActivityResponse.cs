namespace Web.Models
{
    public class ActivityResponse
    {
        public string Activity { get; set; }
        public string Description { get; set; }

        public ActivityResponse(string activity, string description)
        {
            Activity = activity;
            Description = description;
        }
    }
}
