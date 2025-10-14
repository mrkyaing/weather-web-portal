namespace WeatherPortal.Dto
{
    public class AlertViewModel
    {
        public string Id { get; set; }
        public string AlertType { get; set; }
        public string? Message { get; set; }
        public string? WeatherStationId { get; set; }
        public string CityId { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public string StationName { get; set; }
        public string CityNameInEnglish { get; set; }


    }
}
