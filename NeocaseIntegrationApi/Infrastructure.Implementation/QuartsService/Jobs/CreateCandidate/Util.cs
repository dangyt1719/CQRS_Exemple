using Entities;
using NeocaseProviderLibrary.Dto;
using Quartz;

namespace Infrastructure.Implementation.QuartsService.Jobs.CreateCandidate
{
    public static class Util
    {
        public static (string lastName, string firstName, string middleName, string phone, DateTime firstWorkDay, string position,
            string region, string legalPersonPart, string legalPerson, string homeAddress, string privateMailbox, string bossCpId,
            string recruiterCpId, string probationPeriod, int salaryOfficial, int salaryPremium, int salaryTotal, string grade,
            string afterProbationGrade, string contractType, DateTime contractEndingDate, string employmentType, string workingTimeNorm,
            string staffType, string specialization, string employeeActivity, string mvz, Guid regionCenterId, string unitId) GetDataFromDataMap(JobDataMap jobDataMap)
        {
            var lastName = jobDataMap.GetString("LastName");
            var firstName = jobDataMap.GetString("FirstName");
            var middleName = jobDataMap.GetString("MiddleName");
            var phone = jobDataMap.GetString("Phone");
            var firstWorkDay = jobDataMap.GetDateTime("FirstWorkDay");
            var position = jobDataMap.GetString("Position");
            var region = jobDataMap.GetString("Region");
            var legalPersonPart = jobDataMap.GetString("LegalPersonPart");
            var legalPerson = jobDataMap.GetString("LegalPerson");
            var homeAddress = jobDataMap.GetString("HomeAddress");
            var privateMailbox = jobDataMap.GetString("PrivateMailbox");
            var bossCpId = jobDataMap.GetString("BossCpId");
            var recruiterCpId = jobDataMap.GetString("RecruiterCpId");
            var probationPeriod = jobDataMap.GetString("ProbationPeriod");
            var salaryOfficial = jobDataMap.GetInt("SalaryOfficial");
            var salaryPremium = jobDataMap.GetInt("SalaryPremium");
            var salaryTotal = jobDataMap.GetInt("SalaryTotal");
            var grade = jobDataMap.GetString("Grade");
            var afterProbationGrade = jobDataMap.GetString("AfterProbationGrade");
            var contractType = jobDataMap.GetString("ContractType");
            var contractEndingDate = jobDataMap.GetDateTime("ContractEndingDate");
            var employmentType = jobDataMap.GetString("EmploymentType");
            var workingTimeNorm = jobDataMap.GetString("WorkingTimeNorm");
            var staffType = jobDataMap.GetString("StaffType");
            var specialization = jobDataMap.GetString("Specialization");
            var employeeActivity = jobDataMap.GetString("EmployeeActivity");
            var mvz = jobDataMap.GetString("Mvz");
            var rcId = jobDataMap.GetString("ResourceCenter1CId");
            var unitId = jobDataMap.GetString("UnitId");

            var regionCenterId = new Guid(rcId);

            return (lastName, firstName, middleName, phone, firstWorkDay, position, region, legalPersonPart, legalPerson, homeAddress, privateMailbox, bossCpId,
                recruiterCpId, probationPeriod, salaryOfficial, salaryPremium, salaryTotal, grade, afterProbationGrade, contractType, contractEndingDate, employmentType,
                workingTimeNorm, staffType, specialization, employeeActivity, mvz, regionCenterId, unitId);
        }

        public static string ResolveHrDirectoryIdentifier(this HrDirectory dto)
        {
            if (dto is not null && !string.IsNullOrEmpty(dto.HRRegionCenterIdentifier))
                return dto.HRRegionCenterIdentifier;
            if (dto is not null && !string.IsNullOrEmpty(dto.HRMvzIdentifier))
                return dto.HRMvzIdentifier;
            if (dto is not null && !string.IsNullOrEmpty(dto.HRDirectorIdentifier))
                return dto.HRDirectorIdentifier;

            return null;
        }
    }
}