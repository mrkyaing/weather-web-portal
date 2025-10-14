using System.ComponentModel.DataAnnotations.Schema;
using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    [Table("WeatherReadings")]
    public class WeatherReadingEntity:BaseEntity
    {
        public string StationId { get; set; }
        [ForeignKey("StationId")]
        public WeatherStationEntity Station { get; set; }
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

    }
}
