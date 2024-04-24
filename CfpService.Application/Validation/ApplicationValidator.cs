namespace CfpService.Application.Validation
{
    public class ApplicationValidator
    {
        public ValidationResult CheckForNull(CfpService.Domain.Models.Application application)
        {
            if (application == null)
            {
                return new ValidationResult(false, "Заявка не найдена");
            }
            return new ValidationResult(true, "Валидация пройдена успешно");
        }

        public ValidationResult UpdateApplicationSendStatusAsync(Domain.Models.Application application)
        {
            if (application.IsSend == true)
            {

                return new ValidationResult(false,"Нельзя повторно отправить заявку");
            }
            else
            {
                application.IsSend = true;
                application.SendDateTime = DateTime.UtcNow;
                return new ValidationResult(true,"Валидация пройдена успешно");
            }
        }

        public ValidationResult CheckForNullForCreateApplication(bool application)
        {
            if (application == true)
            {
                return new ValidationResult(false, "Валидация пройдена успешно");
            }
            return new ValidationResult(true, "Заявка уже существует");
        }

        public ValidationResult CheckForSend(Domain.Models.Application application)
        {
            if (application.IsSend == true)
            {
                return new ValidationResult(false, "Нельзя обновлять, удалять отправленную заявку");
            }
            else
            {
                return new ValidationResult(true, "Валидация пройдена успешно");
            }
        }

        public ValidationResult ValidateAuthorId(Guid authorId)
        {
            if (authorId == Guid.Empty) 
            {
                return new ValidationResult(false, "Требуется идентификатор автора");
            }
            return new ValidationResult(true, "Валидация прошла успешно");
        }

        public ValidationResult ValidateRequiredFields(Domain.Models.Application application)
        {
            ValidationResult nameValidationResult = ValidateNullOrEmpty(application.Name, "Name");
            ValidationResult outlineValidationResult = ValidateNullOrEmpty(application.Outline, "Outline");

            if (string.IsNullOrEmpty(application.Name) || string.IsNullOrEmpty(application.Outline))
            {
                return new ValidationResult(false, "Заявка должна содержать заполненные поля: Author, Activity, Name и Outline перед отправкой на рассмотрение.");
            }
            return new ValidationResult(true, "Валидация прошла успешно");
        }

        public ValidationResult ValidateNullOrEmpty(string fieldValue, string fieldName)
        {
            fieldValue = fieldValue?.Trim();
            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                return new ValidationResult(false, $"Поле {fieldName} должно быть заполнено перед отправкой на рассмотрение.");
            }
            return new ValidationResult(true, $"Поле {fieldName} прошло успешную валидацию.");
        }
    }
}
