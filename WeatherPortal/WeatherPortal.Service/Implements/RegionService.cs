using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
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

        public async Task Create(RegionViewModel regionVm)
        {
            var entity = new RegionEntity
            {
                Id = Guid.NewGuid().ToString(),
                RegionNameInEnglish = regionVm.RegionNameInEnglish,
                RegionNameInMyanmar = regionVm.RegionNameInMyanmar,
                RegionType = regionVm.RegionType,
                IsActive = true,
                CreatedAt = DateTime.Now,
                Code = regionVm.Code,               
            };
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
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<RegionViewModel>> GetAllRegions()
        {
            var regionEntities = await _unitOfWork.Regions.GetAll();
            return regionEntities.Select(entity => new RegionViewModel
            {
                Id = entity.Id,
                RegionNameInEnglish = entity.RegionNameInEnglish,
                RegionNameInMyanmar = entity.RegionNameInMyanmar,
            }).ToList();
        }

        public async Task<RegionViewModel> GetRegionById(string regionId)
        {
            var regions = await _unitOfWork.Regions.GetBy(r => r.Id == regionId);
            return regions.Select(s => new RegionViewModel
            {
                Id = s.Id,
                RegionNameInEnglish = s.RegionNameInEnglish,
                RegionNameInMyanmar = s.RegionNameInMyanmar,
                RegionType = s.RegionType,
                Code = s.Code
            }).FirstOrDefault();
        }

        public async Task<bool> IsAlreadyExist(string nameInEnglish, string nameInMyanmar, int code)
        {
            return await _unitOfWork.Regions.IsAlreadyExist(nameInEnglish, nameInMyanmar, code);
        }
        public async Task Update(RegionViewModel regionVm)
        {
            var existingRegions = await _unitOfWork.Regions.GetBy(r => r.Id == regionVm.Id);
            var existingRegion = existingRegions.FirstOrDefault();
            if (existingRegion == null)
            {
                throw new Exception("Region not found");
            }

            existingRegion.RegionNameInEnglish = regionVm.RegionNameInEnglish;
            existingRegion.RegionNameInMyanmar = regionVm.RegionNameInMyanmar;
            existingRegion.RegionType = regionVm.RegionType;
            existingRegion.Code = regionVm.Code;
            existingRegion.UpdatedAt = DateTime.Now;
            _unitOfWork.Regions.Update(existingRegion);
            _unitOfWork.Commit();
        }
    }
}
