using Entities;

namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    public interface ILegalDepartmentPriceRepository
    {
        public Task<LigalDepartmentPrice> GetPriceByProcessId(int processId);
    }
}