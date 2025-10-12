using System.ComponentModel.DataAnnotations.Schema;
using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    [Table("WeatherStations")]
    public class WeatherStationEntity:BaseEntity
    {
        public string StationName { get; set; }
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        public CityEntity City { get; set; }
        public string? TownshipId { get; set; }
        [ForeignKey("TownshipId")]
        public TownshipEntity Township { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

    }
}
