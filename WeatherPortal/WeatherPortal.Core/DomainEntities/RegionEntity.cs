using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPortal.Core.BaseEntities;

namespace WeatherPortal.Core.DomainEntities
{
    [Table("Regions")]
    public class RegionEntity : BaseEntity
    {
        public string RegionNameInMyanmar { get; set; }
        public string RegionNameInEnglish { get; set; }
        public string RegionType { get; set; }
        public int Code { get; set; }
        public ICollection<CityEntity> Cities { get; set; }
    }
}
