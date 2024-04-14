using Domain.Models;


namespace CfpService.Application.Validation
{
    public class ActivityValidator
    {
        public ValidationResult CheckForNullActivity(Activity activity)
        {
            if (activity == null)
            {
                return new ValidationResult(false, "Активность не найдена");
            }
            return new ValidationResult(true, "Валидация пройдена успешно");
        }
    }
}
