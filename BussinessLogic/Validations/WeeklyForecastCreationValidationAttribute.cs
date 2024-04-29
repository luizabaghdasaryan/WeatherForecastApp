using BusinessLogic.Interfaces;
using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Validations
{
    public class WeeklyForecastCreationValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null && value is WeeklyForecastInputModel weeklyForecast)
            {
                var service = (IServiceManager)validationContext
                        .GetService(typeof(IServiceManager))!;
                var availableDays = service.ForecastService.GetAvailableDays(weeklyForecast.RegionId);
                if (!availableDays.Any())
                {
                    return new ValidationResult($"All forecast days have already been assigned for the region: {weeklyForecast.RegionId}");
                }

                var missingDays = new List<string>();
                    
                foreach (string date in availableDays)
                {
                    if (!weeklyForecast.DailyForecasts.Any(df => df.Date.ToString("dd-MM-yyyy") == date))
                    {
                        missingDays.Add(date);
                    }
                }

                if (missingDays.Any())
                {
                    return new ValidationResult("Missing daily data for the following dates: " +
                        $"{string.Join(", ", missingDays)}.");
                }
                    return ValidationResult.Success;
            }

            return new ValidationResult("Invalid date value.");
        }
    }
}