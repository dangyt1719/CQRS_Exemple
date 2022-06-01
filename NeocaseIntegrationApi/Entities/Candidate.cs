namespace Entities
{
    public class Candidate
    {
        /// <summary>
        /// Идентификатор оффера в системе
        /// </summary>
        public int OfferId { get; set; }

        /// <summary>
        /// Номер оффера
        /// </summary>
        public string OfferNumber { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? CreatedDateTime { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        public string PhoneSubstitutionByCpId { get; set; }

        /// <summary>
        /// Личная почта
        /// </summary>
        public string PrivateMailbox { get; set; }

        /// <summary>
        /// Домашний адрес
        /// </summary>
        public string HomeAddress { get; set; }

        /// <summary>
        /// Дата выхода на работу
        /// </summary>
        public DateTime? FirstWorkDay { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Идентификатор подразделения
        /// </summary>
        public string UnitId { get; set; }

        /// <summary>
        /// МВЗ
        /// </summary>
        public string Mvz { get; set; }

        /// <summary>
        /// Грейд
        /// </summary>
        public Grade Grade { get; set; }

        /// <summary>
        /// Признак удаленной работы
        /// </summary>
        public bool IsRemoteWork { get; set; }

        /// <summary>
        /// Регион
        /// </summary>
        public Region Region { get; set; }

        /// <summary>
        /// Идентификатор юрлица
        /// </summary>
        public LegalPerson LegalPerson { get; set; }

        /// <summary>
        /// Обособленное подразделение
        /// </summary>
        public LegalPersonPart LegalPersonPart { get; set; }

        /// <summary>
        /// Идентификатор РЦ
        /// </summary>
        public Guid ResourceCenter1CId { get; set; }

        /// <summary>
        /// Регистрация в юрлице другого региона
        /// </summary>
        public bool RegToAlienLegalPersonOfRegion { get; set; }

        /// <summary>
        /// Идентификатор испытательного срока
        /// </summary>
        public ProbationPeriod ProbationPeriod { get; set; }

        /// <summary>
        /// Окончание испытательного срока
        /// </summary>
        public DateTime? ProbationPeriodEndingDate { get; set; }

        /// <summary>
        /// Официальная ЗП
        /// </summary>
        public int SalaryOfficial { get; set; }

        /// <summary>
        /// Премия
        /// </summary>
        public int SalaryPremium { get; set; }

        /// <summary>
        /// Признак наличия премии
        /// </summary>
        public bool? SalaryPremiumEnabled { get; set; }

        /// <summary>
        /// Общая ЗП
        /// </summary>
        public int SalaryTotal { get; set; }

        /// <summary>
        /// Районный коэффициент
        /// </summary>
        public int? RegionalCoefficient { get; set; }

        public decimal? RegionalCoefficientPercentage { get; set; }
        public bool? OkatoHasNorthSurcharge { get; set; }

        /// <summary>
        /// ЗП после исп. срока
        /// </summary>
        public int? AfterProbationSalaryOfficial { get; set; }

        /// <summary>
        /// Премия после исп. срока
        /// </summary>
        public int? AfterProbationSalaryPremium { get; set; }

        /// <summary>
        /// Полная ЗП после имп. срока
        /// </summary>
        public int? AfterProbationSalaryTotal { get; set; }

        /// <summary>
        /// Районый коэффициент после исп. срока
        /// </summary>
        public int? AfterProbationRegionalCoefficient { get; set; }

        /// <summary>
        /// Грейд после исп. срока
        /// </summary>
        public Grade AfterProbationGrade { get; set; }

        /// <summary>
        /// Идентификатор типа трудового договора
        /// </summary>
        public ContractType ContractType { get; set; }

        /// <summary>
        /// Дата окончания трудового тоговора
        /// </summary>
        public DateTime? ContractEndingDate { get; set; }

        /// <summary>
        /// Идентификатор вида занятости (основное/Совместительство)
        /// </summary>
        public EmploymentType EmploymentType { get; set; }

        /// <summary>
        /// Идентификатор производственного графика
        /// </summary>
        public WorkingTimeNorm WorkingTimeNorm { get; set; }

        /// <summary>
        /// Вид производственной функции
        /// </summary>
        public WorkFunction WorkFunction { get; set; }

        /// <summary>
        /// Вид персонала (для сотркдников ДБР)
        /// </summary>
        public StaffType StaffType { get; set; }

        public EmployeeActivity EmployeeActivity { get; set; }

        /// <summary>
        /// Руководитель
        /// </summary>
        public string BossCpId { get; set; }

        /// <summary>
        /// Держатель бюджета
        /// </summary>
        public string BudgetOwnerCpId { get; set; }

        /// <summary>
        /// Рекрутер
        /// </summary>
        public string RecruiterCpId { get; set; }

        /// <summary>
        /// Идентификатор специализации
        /// </summary>
        public Specialization Specialization { get; set; }

        /// <summary>
        /// Таб. номер HR партнера
        /// </summary>
        public string HrPartnerCpId { get; set; }

        /// <summary>
        /// Номер проекта
        /// </summary>
        public int ProjectId { get; set; }
    }
}