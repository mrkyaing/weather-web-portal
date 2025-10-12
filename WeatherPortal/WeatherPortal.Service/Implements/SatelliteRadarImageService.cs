using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{
    public class SatelliteRadarImageService : ISatelliteRadarImageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SatelliteRadarImageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(SatelliteRadarImageViewModel vm)
        {
            var entity = new SatelliteRadarImageEntity
            {
                Id = Guid.NewGuid().ToString(),
                ImageType = vm.ImageType,
                WhenReadAt = vm.WhenReadAt,
                Description = vm.Description,
                IsActive = true
            };
            await _unitOfWork.SatelliteRadarImages.Create(entity);
            _unitOfWork.Commit();
        }

        public async Task Delete(string id)
        {
            var existingEntity = await _unitOfWork.SatelliteRadarImages.GetBy(e => e.Id == id);
            var entity = existingEntity.FirstOrDefault();
            if(entity == null)
            {
                throw new Exception("Satellite/Radar Image not found");
            }
            _unitOfWork.SatelliteRadarImages.Delete(entity);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<SatelliteRadarImageViewModel>> GetAll()
        {
            var entities = await _unitOfWork.SatelliteRadarImages.GetAll();
            return entities.Select(entity => new SatelliteRadarImageViewModel
            {
                Id = entity.Id,
                ImageType = entity.ImageType,
                ImageUrl = entity.ImageUrl,
                WhenReadAt = entity.WhenReadAt,
                Description = entity.Description,
            }).ToList();
        }

        public async Task<SatelliteRadarImageViewModel> GetById(string id)
        {
            var entity = await _unitOfWork.SatelliteRadarImages.GetBy(e => e.Id == id);
            return entity.Select(e => new SatelliteRadarImageViewModel
            {
                Id = e.Id,
                ImageType = e.ImageType,
                ImageUrl = e.ImageUrl,
                WhenReadAt = e.WhenReadAt,
                Description = e.Description,
            }).FirstOrDefault();
        }

        public async Task Update(SatelliteRadarImageViewModel vm)
        {
            var existingEntities = await _unitOfWork.SatelliteRadarImages.GetBy(e => e.Id == vm.Id);
            var entity =  existingEntities.FirstOrDefault();
            if(entity == null)
            {
                throw new Exception("Satellite/Radar Image not found");
            }
            entity.ImageType = vm.ImageType;
            entity.ImageUrl = vm.ImageUrl;
            entity.WhenReadAt = vm.WhenReadAt;
            entity.Description = vm.Description;
            entity.UpdatedAt = DateTime.Now;
            _unitOfWork.SatelliteRadarImages.Update(entity);
            _unitOfWork.Commit();
        }
    }
}
