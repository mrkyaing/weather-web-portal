using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherPortal.Dto
{
    public class WeatherStationViewModel
    {
        public string Id { get; set; } 
        public string StationName { get; set; }
        public string CityId { get; set; }
        public string CityNameInEnglish { get; set; }
        public string? TownshipId { get; set; }
        public string TownshipNameInEnglish { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

    }
}
