namespace Entities
{
    public class PowerAttorneyRegistry
    {
        /// <summary>
        /// Порядковый номер доверенности
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Порядковый номер доверенности
        /// </summary>
        public int PowerAttorneyIndex { get; set; }

        /// <summary>
        /// Год регистрации доверенности
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? DateIssue { get; set; }

        /// <summary>
        /// Номер доверенности
        /// </summary>
        public string PowerAttorneyNumber { get; set; }

        /// <summary>
        /// Вид доверенности
        /// </summary>
        public string PowerAttorneyType { get; set; }

        /// <summary>
        /// Общество-доверитель
        /// </summary>
        public string Principal { get; set; }

        /// <summary>
        /// От чьего имени предоставлены полномочия
        /// </summary>
        public string WhoGranted { get; set; }

        /// <summary>
        /// Перечень полномочий
        /// </summary>
        public string Powers { get; set; }

        /// <summary>
        /// Поверенный (лицо получиышее доверенность)
        /// </summary>
        public string Attorney { get; set; }

        /// <summary>
        /// Срок, на который выдана доверенность
        /// </summary>
        public string Term { get; set; }

        /// <summary>
        /// Инициатор выдачи доверенности (отправляет скан-копию подписанной доверенности)
        /// </summary>
        public string Initiator { get; set; }
    }
}