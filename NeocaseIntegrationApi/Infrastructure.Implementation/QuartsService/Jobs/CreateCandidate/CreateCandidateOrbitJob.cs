using Infrastructure.Interfaces.QuartsServiceInterfaces;
using Infrastructure.Interfaces.RepositoryInterfaces;
using Microsoft.Extensions.Logging;
using NeocaseProviderLibrary.Model;
using NeocaseProviderLibrary.Providers;
using Quartz;

namespace Infrastructure.Implementation.QuartsService.Jobs.CreateCandidate
{
    [DisallowConcurrentExecution]
    public class CreateCandidateOrbitJob : ICreateCandidateOrbitJob
    {
        private readonly ILogger<CreateCandidateOrbitJob> _logger;
        private readonly NeocaseRootProvider _neocase;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHrDirectoryRepository _hrDirectoryRepository;
        private readonly IRegionCenterRepository _regionCenterRepository;

        public CreateCandidateOrbitJob(ILogger<CreateCandidateOrbitJob> logger, NeocaseRootProvider neocase, IEmployeeRepository employeeRepository, IHrDirectoryRepository hrDirectoryRepository, IRegionCenterRepository regionCenterRepository)
        {
            _logger = logger;
            _neocase = neocase;
            _employeeRepository = employeeRepository;
            _hrDirectoryRepository = hrDirectoryRepository;
            _regionCenterRepository = regionCenterRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Start create orbit candidate job");

            var (lastName, firstName, middleName, phone,
               firstWorkDay, position, region, legalPersonPart,
               legalPerson, homeAddress, privateMailbox, bossCpId,
               recruiterCpId, probationPeriod, salaryOfficial, salaryPremium,
               salaryTotal, grade, afterProbationGrade, contractType, contractEndingDate,
               employmentType, workingTimeNorm, staffType, specialization, employeeActivity,
               mvz, regionCenterId, unitId) = Util.GetDataFromDataMap(context.MergedJobDataMap);

            var hrDirectoryTask = _hrDirectoryRepository.GetHrDirectoryByRCMvz(regionCenterId, mvz);
            var bossTask = _employeeRepository.GetEmployeeByCpId(bossCpId);
            var recruiterTask = _employeeRepository.GetEmployeeByCpId(recruiterCpId);

            await Task.WhenAll(hrDirectoryTask, bossTask, recruiterTask);

            var structureTask = _neocase.NeocaseDbProvider.GetStructureById(unitId);
            var regionCenterTask = _regionCenterRepository.GetStructureByRcId(regionCenterId);

            var hrDirectoryIdentifier = hrDirectoryTask.Result.ResolveHrDirectoryIdentifier();
            var hrPartnerContactTask = _neocase.NeocaseDbProvider.GetContactByIdentifier(hrDirectoryIdentifier);

            await Task.WhenAll(structureTask, regionCenterTask, hrPartnerContactTask);

            var caseModel = new CaseModel
            {
                ContactId = hrPartnerContactTask.Result is not null ? hrPartnerContactTask.Result.ContactId : 0,
                StepId = 1000386,
                ProcessId = 1000384
            };

            caseModel.Champi[15] = firstWorkDay.ToString("yyyyMMddHHmmss");
            caseModel.Champi[19] = contractEndingDate == DateTime.MinValue ? null : contractEndingDate.ToString("yyyyMMddHHmmss");
            caseModel.Champi[29] = salaryOfficial.ToString();
            caseModel.Champi[30] = afterProbationGrade;
            caseModel.Champi[31] = position;
            caseModel.Champi[33] = structureTask.Result is not null ? structureTask.Result.Title : string.Empty;
            caseModel.Champi[35] = lastName;
            caseModel.Champi[36] = firstName;
            caseModel.Champi[37] = middleName;
            caseModel.Champi[38] = phone;
            caseModel.Champi[39] = legalPerson;
            caseModel.Champi[40] = regionCenterTask.Result.Title;
            caseModel.Champi[41] = homeAddress;
            caseModel.Champi[42] = privateMailbox;
            caseModel.Champi[43] = recruiterTask.Result.Name;
            caseModel.Champi[44] = hrPartnerContactTask.Result is not null ? $"{hrPartnerContactTask.Result.LastName} {hrPartnerContactTask.Result.FirstName}" : null;
            caseModel.Champi[46] = contractType;
            caseModel.Champi[47] = employmentType;
            caseModel.Champi[48] = workingTimeNorm;
            caseModel.Champi[49] = salaryPremium.ToString();
            caseModel.Champi[106] = legalPersonPart;
            caseModel.Champi[107] = probationPeriod;
            caseModel.Champi[180] = region;
            caseModel.Champi[181] = bossTask.Result.Name;
            caseModel.Champi[182] = salaryTotal.ToString();
            caseModel.Champi[183] = grade;
            caseModel.Champi[184] = staffType;
            caseModel.Champi[185] = specialization;
            caseModel.Champi[186] = employeeActivity;

            var newCaseId = await _neocase.NeocaseCaseProvider.CreateCase(caseModel);

            _ = await _neocase.NeocaseCaseProvider.ExecuteRule(newCaseId, 1632);

            var recruiterContactId = await _neocase.NeocaseContactProvider.GetContactIdByIdentifier(recruiterTask.Result.Identifier);

            var administator = 6157; // Заглушка администратора

            var notificationList = new List<long>
            {
                recruiterContactId,
                hrPartnerContactTask.Result.ContactId,
                administator
            };
            await _neocase.NeocaseCaseProvider.AddContactToCaseNotificationList(newCaseId, notificationList);
        }
    }
}