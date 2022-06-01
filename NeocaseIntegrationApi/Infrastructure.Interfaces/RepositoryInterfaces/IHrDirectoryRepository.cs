using Entities;

namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    public interface IHrDirectoryRepository
    {
        public Task<HrDirectory> GetHrDirectoryByRCMvz(Guid rcId, string mvz);
    }
}