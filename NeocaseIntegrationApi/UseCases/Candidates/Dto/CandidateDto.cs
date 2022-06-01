namespace UseCases.Candidates.Dto
{
    public class CandidateDto
    {
        public int OfferId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public DateTime FirstWorkDay { get; set; }
        public PositionDto Position { get; set; }
        public LegalPersonPartDto LegalPersonPart { get; set; }
        public GradeDto AfterProbationGrade { get; set; }
        public RegionDto Region { get; set; }
        public LegalPersonDto LegalPerson { get; set; }
        public string HomeAddress { get; set; }
        public string PrivateMailbox { get; set; }
        public string BossCpId { get; set; }
        public string RecruiterCpId { get; set; }
        public string HrPartnerCpId { get; set; }
        public ProbationPeriodDto ProbationPeriod { get; set; }
        public int SalaryOfficial { get; set; }
        public int SalaryPremium { get; set; }
        public int SalaryTotal { get; set; }
        public GradeDto Grade { get; set; }
        public ContractTypeDto ContractType { get; set; }
        public DateTime? ContractEndingDate { get; set; }
        public WorkFunctionDto WorkFunction { get; set; }
        public EmploymentTypeDto EmploymentType { get; set; }
        public WorkingTimeNormDto WorkingTimeNorm { get; set; }
        public StaffTypeDto StaffType { get; set; }
        public SpecializationDto Specialization { get; set; }
        public EmployeeActivityDto EmployeeActivity { get; set; }
        public Guid ResourceCenter1CId { get; set; }
        public string Mvz { get; set; }
        public string UnitId { get; set; }
    }
}