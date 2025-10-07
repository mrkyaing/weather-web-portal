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
        }

        public Task Delete(string regionId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RegionEntity>> GetAllRegions()
        {
            throw new NotImplementedException();
        }

        public Task<RegionEntity> GetRegionById(string regionId)
        {
            throw new NotImplementedException();
        }

        public bool IsAlreadyExist(RegionEntity regionViewModel)
        {
            throw new NotImplementedException();
        }

        public Task Update(RegionEntity regionViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
