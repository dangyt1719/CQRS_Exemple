using Entities;

namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    public interface IEmployeeRepository
    {
        public Task<Employee> GetEmployeeByCpId(string cpId);
    }
}