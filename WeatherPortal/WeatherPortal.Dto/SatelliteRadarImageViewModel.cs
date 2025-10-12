namespace WeatherPortal.Dto
{
    public class SatelliteRadarImageViewModel
    {
        public string Id { get; set; }
        public string ImageType { get; set; }
        public string ImageUrl { get; set; }
        public DateTime WhenReadAt { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
