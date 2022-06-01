using Entities;

namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    public interface IPowerAttorneyRegistryRepository
    {
        public Task<IEnumerable<PowerAttorneyRegistry>> GetPowerAttorneyRegistriesAsync();

        public byte[] GetPowerAttorneyRegistriesReport(IEnumerable<PowerAttorneyRegistry> registries);
    }
}