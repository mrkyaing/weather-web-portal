namespace WeatherPortal.Dto
{
    public class WeatherReadingViewModel
    {
        public string Id { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }
        public DateTime? WhenReadAt { get; set; }
        public decimal? TemperatureMax { get; set; }
        public decimal? TemperatureMin { get; set; }
        public decimal? Pressure { get; set; }
        public decimal? Humidity { get; set; }
        public decimal? WindSpeed { get; set; }
        public string? WindDirection { get; set; }
        public decimal? Rainfall { get; set; }
        public string? PresentWeather { get; set; }
        public string? SeaWeather { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
