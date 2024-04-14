using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;



namespace CfpService.Application.Validation
{
    public class ApplicationValidator
    {
        public ValidationResult CheckForNull(Domain.Models.Application application)
        {
            if (application == null)
            {
                return new ValidationResult(false, "Заявка не найдена");
            }
            return new ValidationResult(true, "Валидация пройдена успешно");
        }

        
    }
}
