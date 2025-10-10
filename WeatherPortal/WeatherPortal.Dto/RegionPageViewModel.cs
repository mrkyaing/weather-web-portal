
namespace WeatherPortal.Dto
{
    public class RegionPageViewModel
    {
        public RegionViewModel Region { get; set; } // for Region Entry form
        public IEnumerable<RegionViewModel> RegionList { get; set; } // for list Table 
    }
}
