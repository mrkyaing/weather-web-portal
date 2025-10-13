using System.ComponentModel.DataAnnotations;

namespace WeatherPortal.Dto
{
    public class SatelliteRadarImageViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Image Type is required")]
        [StringLength(100, ErrorMessage = "Image Type cannot exceed 100 characters")]
        public string ImageType { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        public DateTime WhenReadAt { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}