<<<<<<< HEAD
﻿namespace WeatherPortal.DataModel.BaseEntities
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool  IsActive { get; set; }=true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } 
=======
﻿using System.ComponentModel.DataAnnotations;

namespace WeatherPortal.Core.BaseEntities
{
    public abstract class BaseEntity
    {
        [Key]
        public string Id { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; }

>>>>>>> Geographic_Structure_Setup_24
    }
}
