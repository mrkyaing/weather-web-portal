using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Service.Interfaces;
namespace WeatherPortal.Service.Implements
{
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(RegionEntity entity)
        {
            await _unitOfWork.Regions.Create(entity);
            _unitOfWork.Commit();
        }

        public async Task Delete(string regionId)
        {
            var existingRegions = await _unitOfWork.Regions.GetBy(r => r.Id.Equals(regionId));
            var existingRegion = existingRegions.FirstOrDefault();
            if (existingRegion == null)
            {
                throw new Exception("Region not found");
            }
            _unitOfWork.Regions.Delete(existingRegion);
        }
        public bool IsAlreadyExist(string nameInEnglish, string nameInMyanmar, int code)
        {
           var existingRegions =  _unitOfWork.Regions.GetBy(r => r.RegionNameInEnglish == nameInEnglish || 
                                                                 r.RegionNameInMyanmar == nameInMyanmar ||
                                                                 r.Code == code).Result;
            return existingRegions.Any();
        }

        public async Task Update(RegionEntity entity)
        {
            var existingRegions = await _unitOfWork.Regions.GetBy(r => r.Id == entity.Id);
            var existingRegion = existingRegions.FirstOrDefault();
            if (existingRegion == null)
            {
                throw new Exception("Region not found");
            }

            existingRegion.RegionNameInEnglish = entity.RegionNameInEnglish;
            existingRegion.RegionNameInMyanmar = entity.RegionNameInMyanmar;
            existingRegion.RegionType = entity.RegionType;
            existingRegion.Code = entity.Code;
            existingRegion.UpdatedAt = DateTime.Now;
            _unitOfWork.Regions.Update(existingRegion);
            _unitOfWork.Commit();
        }
    }
}
