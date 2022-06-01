namespace Entities
{
    public class LigalDepartmentPrice
    {
        public int ProcessId { get; set; }
        public string ProcessName { get; set; }

        /// <summary>
        /// Соответствует полю "Служебная необходимость Категория 1" в файлу excel
        /// </summary>
        public int InternalCategory1Price { get; set; }

        /// <summary>
        /// Соответствует полю "Служебная необходимость Категория 2" в файлу excel
        /// </summary>
        public int InternalCategory2Price { get; set; }

        public int NotarialServicesPrice { get; set; }

        /// <summary>
        /// Соответствует полю "Внешний консалтинг Категория 1" в файлу excel
        /// </summary>
        public int ExternalCategory1Price { get; set; }

        /// <summary>
        /// Соответствует полю "Внешний консалтинг Категория 2" в файлу excel
        /// </summary>
        public int ExternalCategory2Price { get; set; }
    }
}