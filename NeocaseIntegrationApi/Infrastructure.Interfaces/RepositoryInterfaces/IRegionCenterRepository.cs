using Entities;

namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    public interface IRegionCenterRepository
    {
        public Task<RegionCenter> GetStructureByRcId(Guid guid);
    }
}