using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Data;

namespace WeatherPortal.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IRegionRepository Regions { get; }
        public ICityRepository Cities { get; }

        public UnitOfWork(ApplicationDbContext dbContext,
                          IRegionRepository regionRepository,
                          ICityRepository cityRepository ) 
        {
            _dbContext = dbContext;
            Regions = regionRepository;
            Cities = cityRepository;
        }
         
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void RollBack()
        {
            _dbContext.Database.CurrentTransaction?.Rollback();
        }
    }
}
