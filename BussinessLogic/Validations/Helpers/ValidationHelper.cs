using Shared.Models;

namespace BusinessLogic.Validations.Helpers
{
    public static class ValidationHelper
    {
        public static string? ValidateHourlyDetails(List<HourlyForecastInputModel> hourlyForecasts)
        {
            string? message = null;
            var missingHours = new List<int>();
            var redundant = new List<int>();
            for (int i = 0; i < 2; ++i)
            {
                try
                {
                    if (hourlyForecasts.SingleOrDefault(x => x.Hour == i) is null)
                    {
                        missingHours.Add(i);
                    }
                }
                catch (InvalidOperationException)
                {
                    redundant.Add(i);
                }
            }

            if (redundant.Any())
            {
                message = "Hourly forecasts contain redundant data for the following hours: " +
                    $"{string.Join(":00, ", redundant)}:00.";
            }

            if (missingHours.Any())
            {
                message = "Missing hourly data for the following hours: " +
                    $"{string.Join(":00, ", missingHours)}:00.";
            }

            return message;
        }

        public static string? ValidateForecastDate(DateTime date)
        {
            string? message = null;
            DateTime minDate = DateTime.Today.AddDays(1);
            DateTime maxDate = DateTime.Today.AddDays(7);

            if (date.Date < minDate || date.Date > maxDate)
            {
                message = $"The date must be within approximately 7 days from today.";
            }

            return message;
        }
    } 
}