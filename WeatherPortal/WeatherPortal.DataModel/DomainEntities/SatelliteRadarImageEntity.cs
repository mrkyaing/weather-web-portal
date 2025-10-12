using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    public class SatelliteRadarImageEntity:BaseEntity
    {
        public string ImageType { get; set; }
        public string ImageUrl { get; set; }
        public DateTime WhenReadAt { get; set; }
        public string Description { get; set; }
    }
}
