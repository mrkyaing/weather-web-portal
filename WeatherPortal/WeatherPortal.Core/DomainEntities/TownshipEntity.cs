using System.ComponentModel.DataAnnotations.Schema;
using WeatherPortal.Core.BaseEntities;

namespace WeatherPortal.Core.DomainEntities
{
    [Table("Townships")]
    public class TownshipEntity:BaseEntity
    {
        public string Id { get; set; }
        public string TownshipNameInMyanmar { get; set; }
        public string TownshipNameInEnglish { get; set; }
        public string CityId { get; set; }
        [ForeignKey("CityId")]
        public CityEntity Cities { get; set; }
    }
}
