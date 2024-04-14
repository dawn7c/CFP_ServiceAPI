using Domain.Abstractions;
using Domain.Models;

namespace CfpService.DataAccess.ActivityRepository
{
    public class ActivityRepository : IActivity
    {

        public async Task<List<object>> ListOfActivityWithDescriptionAsync()
        {
            var activities = new List<object>();

            foreach (Activity activity in Enum.GetValues(typeof(Activity)))
            {
                string description = ActivityDescription(activity);
                activities.Add(new { activity = activity.ToString(), description });
            }

            return activities;
        }

        private string ActivityDescription(Activity activity)
        {
            switch (activity)
            {
                case Activity.Report:
                    return "Доклад, 35-45 минут";
                case Activity.Masterclass:
                    return "Мастеркласс, 1-2 часа";
                case Activity.Discussion:
                    return "Дискуссия / круглый стол, 40-50 минут";
                default:
                    return string.Empty;
            }
        }
    }
}
