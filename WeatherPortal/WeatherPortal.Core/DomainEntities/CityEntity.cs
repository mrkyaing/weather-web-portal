<<<<<<< HEAD
﻿using WeatherPortal.DataModel.BaseEntities;

namespace WeatherPortal.DataModel.DomainEntities
{
    public class CityEntity: BaseEntity
    {
=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPortal.Core.BaseEntities;

namespace WeatherPortal.Core.DomainEntities
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

>>>>>>> Geographic_Structure_Setup_24
    }
}
