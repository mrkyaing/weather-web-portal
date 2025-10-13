using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{
    public class NewsService : INewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(NewsViewModel newsViewModel)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(newsViewModel.Title))
                    throw new ArgumentException("Title is required");

                if (string.IsNullOrWhiteSpace(newsViewModel.Content))
                    throw new ArgumentException("Content is required");

                if (string.IsNullOrWhiteSpace(newsViewModel.Type))
                    throw new ArgumentException("Type is required");

                if (string.IsNullOrWhiteSpace(newsViewModel.WeatherStationId))
                    throw new ArgumentException("Weather station is required");

                // Limit Type length to 10 characters to match database
                var type = newsViewModel.Type.Trim();
                if (type.Length > 10)
                {
                    type = type.Substring(0, 10);
                }

                var entity = new NewsEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Content = newsViewModel.Content.Trim(),
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                    IsPublic = newsViewModel.IsPublic,
                    PublishedAt = DateTime.Now,
                    Title = newsViewModel.Title.Trim(),
                    Type = type, // use trancate
                    WeatherStationId = newsViewModel.WeatherStationId,
                    UpdatedAt = DateTime.Now
                };

                await _unitOfWork.News.Create(entity);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new Exception("Error occurred in Creating News: " + e.Message);
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var existingNews = await _unitOfWork.News.GetBy(n => n.Id == id);
                var news = existingNews.FirstOrDefault();
                if (news != null)
                {
                    _unitOfWork.News.Delete(news);
                    _unitOfWork.Commit();
                }
                else
                {
                    throw new Exception("News not found");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Cannot delete news: " + e.Message);
            }
        }

        public async Task<IEnumerable<NewsViewModel>> GetAll()
        {
            var news = (from n in await _unitOfWork.News.GetAll()
                        join w in await _unitOfWork.WeatherStations.GetAll()
                        on n.WeatherStationId equals w.Id
                        where n.IsActive
                        select new NewsViewModel
                        {
                            Id = n.Id,
                            WeatherStationId = w.Id,
                            WeatherStationName = w.StationName,
                            Content = n.Content,
                            Title = n.Title,
                            Type = n.Type,
                            IsPublic = n.IsPublic
                        }).ToList();
            return news;
        }

        public async Task<NewsViewModel> GetById(string id)
        {
            var news = await _unitOfWork.News.GetBy(n => n.Id == id && n.IsActive);
            return news.Select(s => new NewsViewModel
            {
                Id = s.Id,
                WeatherStationId = s.WeatherStationId,
                Type = s.Type,
                Title = s.Title,
                Content = s.Content,
                IsPublic = s.IsPublic
            }).SingleOrDefault();
        }

        public async Task<IEnumerable<NewsViewModel>> GetNewsByWeatherStation(string weatherStationId)
        {
            var news = await _unitOfWork.News.GetNewsByWeatherStation(weatherStationId);
            var result = news.Where(n => n.IsActive).Select(n => new NewsViewModel
            {
                Id = n.Id,
                WeatherStationId = n.WeatherStationId,
                Type = n.Type,
                Title = n.Title,
                Content = n.Content,
                IsPublic = n.IsPublic
            }).ToList();
            return result;
        }

        public async Task Update(NewsViewModel newsViewModel)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(newsViewModel.Title))
                throw new ArgumentException("Title is required");

            if (string.IsNullOrWhiteSpace(newsViewModel.Content))
                throw new ArgumentException("Content is required");

            if (string.IsNullOrWhiteSpace(newsViewModel.WeatherStationId))
                throw new ArgumentException("Weather station is required");

            var newsEntity = await _unitOfWork.News.GetBy(n => n.Id == newsViewModel.Id && n.IsActive);
            var news = newsEntity.FirstOrDefault();

            if (news == null)
            {
                throw new Exception("News not found to update");
            }

            news.Title = newsViewModel.Title.Trim();
            news.Content = newsViewModel.Content.Trim();
            news.WeatherStationId = newsViewModel.WeatherStationId;
            news.Type = newsViewModel.Type?.Trim();
            news.PublishedAt = DateTime.Now;
            news.IsPublic = newsViewModel.IsPublic;
            news.UpdatedAt = DateTime.Now;

            _unitOfWork.News.Update(news);
            _unitOfWork.Commit();
        }
    }
}