using WeatherPortal.DataModel.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherPortal.DataModel.DomainEntities
{
    [Table("Cities")]
    public class CityEntity:BaseEntity
    {
        public string CityNameInMyanmar { get; set; }
        public string CityNameInEnglish { get; set; }
        public string RegionId { get; set; }
        [ForeignKey("RegionId")]
        public RegionEntity Region { get; set; }
        public ICollection<TownshipEntity> Township { get; set; }
    }
}
