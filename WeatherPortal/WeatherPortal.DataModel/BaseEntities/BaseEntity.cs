namespace WeatherPortal.DataModel.BaseEntities;
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public bool  IsActive { get; set; }=true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } 
}
