using System.ComponentModel.DataAnnotations;

namespace WeatherPortal.Dto
{
    public class NewsViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        [StringLength(2000, ErrorMessage = "Content cannot exceed 2000 characters")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime PublishedAt { get; set; } = DateTime.Now;

        public bool IsPublic { get; set; } = true;

        [Required(ErrorMessage = "Weather station is required")]
        public string WeatherStationId { get; set; }

        public string WeatherStationName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}