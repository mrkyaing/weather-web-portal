namespace WeatherPortal.Dto
{
    public class CityViewModel
    {
        public string Id { get; set; }
        public  string CityNameInEnglish { get; set; }
        public string CityNameInMyanmar { get; set; }
        public string RegionId { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;
    }
}
