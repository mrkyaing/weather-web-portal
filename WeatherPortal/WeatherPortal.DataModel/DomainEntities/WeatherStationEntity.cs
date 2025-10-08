using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    public class WeatherStationEntity:BaseEntity
    {
        public string StationName { get; set; }
        public string TownshipId { get; set; }
    }
}
