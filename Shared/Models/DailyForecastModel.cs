namespace Shared.Models
{
    public class DailyForecastModel
    {
        public int Id { get; set; }

        public string Date { get; set; } = string.Empty;

        public string DayWithMonth { get; set; } = string.Empty;

        public string DayOfWeek { get; set; } = string.Empty;

        public int MinTemperature { get; set; }

        public int MaxTemperature { get; set; }

        public string Summary { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;
    }
}