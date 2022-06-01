namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    public interface IPermissionRepositiry
    {
        public Task<bool> HasPermissionForEmpMoneyAsync(string pernr, string userLogin, int[] roles);
    }
}
