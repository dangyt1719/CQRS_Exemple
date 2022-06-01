using Entities;

namespace Infrastructure.Interfaces.RepositoryInterfaces
{
    /// <summary>
    /// Репозиторий для работы со справчником подразделений в БД Неокейс
    /// </summary>
    public interface IOrgUnitRepository
    {
        /// <summary>
        /// Получить список подразделений, содержащих в своем названии текст <paramref name="namePattern"/>
        /// </summary>
        /// <param name="namePattern">Строка для поиска</param>
        /// <param name="maxCount">Максимальное кол-во возращаемых результатов</param>
        /// <returns>Информация о подразделении</returns>
        public Task<IEnumerable<OrgUnit>> GetOrgList(string namePattern, int maxCount = 50);

        public Task<OrgUnit> GetMvzInfo(string code);

        public Task<string> GetRcByPernr(string pernr);

        public Task<IEnumerable<OrgUnit>> GetAllOrgUnitsAsync();

        public Task<CaseFielsd> GetCaseFields(string caseNum);
    }
}