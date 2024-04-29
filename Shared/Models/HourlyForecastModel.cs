namespace Shared.Models
{
    public class HourlyForecastModel
    {
        public int Id { get; set; }

        public string Hour { get; set; } = string.Empty;

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
