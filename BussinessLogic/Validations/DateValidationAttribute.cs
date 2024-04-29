using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Validations
{
    public class DateValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null && value is DateTime dateValue)
            {
                DateTime minDate = DateTime.Today.AddDays(1);
                DateTime maxDate = DateTime.Today.AddDays(7);

                if (dateValue >= minDate && dateValue <= maxDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"The date must be within approximately 7 days from today.");
                }
            }

            return new ValidationResult("Invalid date value.");
        }
    }
}
