using System.ComponentModel.DataAnnotations.Schema;
using WeatherPortal.DataModel.BaseEntities;
namespace WeatherPortal.DataModel.DomainEntities
{
    [Table("Regions")]
    public class RegionEntity : BaseEntity
    {
        public string RegionNameInMyanmar { get; set; }
        public string RegionNameInEnglish { get; set; }
        public string RegionType { get; set; }
        public int OrderCode { get; set; }
        public ICollection<CityEntity> Cities { get; set; }
    }
}
