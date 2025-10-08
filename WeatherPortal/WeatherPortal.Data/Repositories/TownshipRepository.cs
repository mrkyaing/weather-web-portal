using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class TownshipRepository:BaseRepository<TownshipEntity>,ITownshipRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TownshipRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TownshipEntity> GetTownshipByCity(string cityId)
        {
            return _dbContext.Townships.Where(w => w.CityId == cityId && w.IsActive)
                                        .Select(s => new TownshipEntity
                                        {
                                            Id = s.Id,
                                            CityId = s.CityId,
                                            TownshipNameInEnglish = s.TownshipNameInEnglish,
                                            TownshipNameInMyanmar = s.TownshipNameInMyanmar
                                        }).ToList();
        }

        public IEnumerable<TownshipEntity> GetTownships()
        {
            return _dbContext.Townships.Select(s => new TownshipEntity
            {
                Id = s.Id,
                CityId = s.CityId,
                TownshipNameInEnglish = s.TownshipNameInEnglish,
                TownshipNameInMyanmar = s.TownshipNameInMyanmar
            }).ToList();
        }

        public bool IsExistingTownship(string townshipNameInEnglish, string townshipNameInMyanmar)
        {
           return _dbContext.Townships.Where(t =>t.TownshipNameInEnglish == townshipNameInEnglish &&
                                                  t.TownshipNameInMyanmar == townshipNameInMyanmar)
                                      .Any();
        }
    }
}
