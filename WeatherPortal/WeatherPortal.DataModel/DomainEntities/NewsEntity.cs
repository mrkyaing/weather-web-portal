using System.ComponentModel.DataAnnotations.Schema;
using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    public class NewsEntity:BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime PublicAt { get; set; }
        public bool IsPublic { get; set; }
        public string WeatherStationId { get; set; }
        [ForeignKey(nameof(WeatherStationId))]
        public WeatherStationEntity WeatherStation{ get; set; }


    }
}
