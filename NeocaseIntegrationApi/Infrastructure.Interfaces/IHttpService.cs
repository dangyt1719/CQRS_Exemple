namespace Infrastructure.Interfaces
{
    public interface IHttpService
    {
        public Task<T> GetDataFromAPIAsync<T>(string requestUri = default);
    }
}