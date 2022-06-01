
namespace Entities
{
    /// <summary>
    /// Подразделение в компании
    /// </summary>
    public class OrgUnit
    {
        /// <summary>
        /// Код подразделения
        /// </summary>
        public long Code { get; set; }


        /// <summary>
        /// Название подразделения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь внутри оргструктуры
        /// </summary>
        public string FullOrgPath { get; set; }

        /// <summary>
        /// Имя линейного руководителя структуры
        /// </summary>
        public string BossName { get; set; }

        /// <summary>
        /// Табельный номер лин руководителя структуры
        /// </summary>
        public string BossPernr { get; set; }

        /// <summary>
        /// Код МВЗ
        /// </summary>
        public string MvzCode { get; set; }

        /// <summary>
        /// Наименование МВЗ
        /// </summary>
        public string MvzName { get; set; }

    }
}
