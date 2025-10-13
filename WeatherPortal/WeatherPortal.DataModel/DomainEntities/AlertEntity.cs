using System.ComponentModel.DataAnnotations.Schema;
using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    [Table("Alerts")]
    public class AlertEntity:BaseEntity
    {
        public string AlertType { get; set; }
        public string? Message { get; set; }
        public string? WeatherStationId { get; set; }
        [ForeignKey("WeatherStationId")]
        public WeatherStationEntity WeatherStation { get; set; }
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        public CityEntity City { get; set; }

    }
}
