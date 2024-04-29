using BusinessLogic.Interfaces;
using BusinessLogic.Validations.Helpers;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Validations
{
    public class DailyForecastCreationValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not null && value is DailyForecastInputModel dailyForecast)
            {
                var error = ValidationHelper.ValidateForecastDate(dailyForecast.Date);
                if (error is not null)
                {
                    return new ValidationResult(error);
                }
                
                var service = (IServiceManager)validationContext
                        .GetService(typeof(IServiceManager))!;
                var availableDays = service.ForecastService.GetAvailableDays(dailyForecast.RegionId);
                string formattedDate = dailyForecast.Date.ToString("dd-MM-yyyy");
                if (!availableDays.Any())
                {
                    return new ValidationResult($"All forecast days have already been assigned for the region {dailyForecast.RegionId}");
                }

                if (availableDays.Contains(formattedDate))
                {
                    var errorMessage = ValidationHelper.ValidateHourlyDetails(dailyForecast.HourlyForecasts);
                    if (errorMessage != null)
                    {
                        return new ValidationResult(errorMessage);
                    }

                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"The forecast for {formattedDate} has already been added. " +
                        $"Available days: {string.Join(", ", availableDays)}.");
                }
            }

            return new ValidationResult("Invalid date value.");
        }
    }
}