using Infrastructure.Interfaces.QuartsServiceInterfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using UseCases.Candidates.Dto;

namespace UseCases.Candidates.Commands.CreateCandidateToNeocase;

public class CreateCandidateToNeocaseCommandHandler : IRequestHandler<CreateCandidateToNeocaseCommand, bool>
{
    private readonly ILogger<CreateCandidateToNeocaseCommandHandler> _logger;

    private readonly ISchedulerFactory _schedulerFactory;

    public CreateCandidateToNeocaseCommandHandler(ILogger<CreateCandidateToNeocaseCommandHandler> logger, ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    public async Task<bool> Handle(CreateCandidateToNeocaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCandidateToNeocaseCommandHandler");
        var scheduler = await _schedulerFactory.GetScheduler(cancellationToken);

        var jobIdentity = request.Candidate.OfferId.ToString();

        IJobDetail newJob;

        if (request.Candidate.ResourceCenter1CId == new Guid("2f53f5af-d1f4-11eb-a2d0-0050569dce03"))
        {
            newJob = JobBuilder.Create<ICreateCandidateOrbitJob>()
                                    .WithIdentity(jobIdentity)
                                    .RequestRecovery(true)
                                    .Build();
        }
        else
        {
            newJob = JobBuilder.Create<ICreateCandidateJob>()
                                    .WithIdentity(jobIdentity)
                                    .RequestRecovery(true)
                                    .Build();
        }

        FillJobDataMap(newJob, request.Candidate);

        var trigger = TriggerBuilder.Create().WithIdentity(jobIdentity).ForJob(newJob).StartNow().Build();
        await scheduler.ScheduleJob(newJob, trigger, cancellationToken);

        _logger.LogInformation("CreateCandidateToNeocaseCommandHandler");

        return true;
    }

    private static void FillJobDataMap(IJobDetail job, CandidateDto candidate)
    {
        job.JobDataMap.Clear();
        job.JobDataMap.Add(nameof(candidate.LastName), candidate.LastName);
        job.JobDataMap.Add(nameof(candidate.FirstName), candidate.FirstName);
        job.JobDataMap.Add(nameof(candidate.MiddleName), candidate.MiddleName);
        job.JobDataMap.Add(nameof(candidate.Phone), candidate.Phone);
        job.JobDataMap.Add(nameof(candidate.FirstWorkDay), candidate.FirstWorkDay.Date);
        job.JobDataMap.Add(nameof(candidate.Position), candidate.Position.Title);
        job.JobDataMap.Add(nameof(candidate.LegalPersonPart), candidate.LegalPersonPart.Title);
        job.JobDataMap.Add(nameof(candidate.AfterProbationGrade), candidate.AfterProbationGrade.Title);
        job.JobDataMap.Add(nameof(candidate.Region), candidate.Region.Title);
        job.JobDataMap.Add(nameof(candidate.LegalPerson), candidate.LegalPerson.Title);
        job.JobDataMap.Add(nameof(candidate.HomeAddress), candidate.HomeAddress);
        job.JobDataMap.Add(nameof(candidate.PrivateMailbox), candidate.PrivateMailbox);
        job.JobDataMap.Add(nameof(candidate.BossCpId), candidate.BossCpId);
        job.JobDataMap.Add(nameof(candidate.RecruiterCpId), candidate.RecruiterCpId);
        job.JobDataMap.Add(nameof(candidate.HrPartnerCpId), candidate.HrPartnerCpId);
        job.JobDataMap.Add(nameof(candidate.ProbationPeriod), candidate.ProbationPeriod.Title);
        job.JobDataMap.Add(nameof(candidate.SalaryOfficial), candidate.SalaryOfficial);
        job.JobDataMap.Add(nameof(candidate.SalaryPremium), candidate.SalaryPremium);
        job.JobDataMap.Add(nameof(candidate.SalaryTotal), candidate.SalaryTotal);
        job.JobDataMap.Add(nameof(candidate.Grade), candidate.Grade.Title);
        job.JobDataMap.Add(nameof(candidate.ContractType), candidate.ContractType.Title);
        job.JobDataMap.Add(nameof(candidate.ContractEndingDate), candidate.ContractEndingDate?.Date);
        job.JobDataMap.Add(nameof(candidate.WorkFunction), candidate.WorkFunction.Title);
        job.JobDataMap.Add(nameof(candidate.EmploymentType), candidate.EmploymentType.Title);
        job.JobDataMap.Add(nameof(candidate.WorkingTimeNorm), candidate.WorkingTimeNorm.Title);
        job.JobDataMap.Add(nameof(candidate.StaffType), candidate.StaffType.Title);
        job.JobDataMap.Add(nameof(candidate.Specialization), candidate.Specialization.Title);
        job.JobDataMap.Add(nameof(candidate.EmployeeActivity.ActivityLocation), candidate.EmployeeActivity.ActivityLocation);
        job.JobDataMap.Add(nameof(candidate.EmployeeActivity), candidate.EmployeeActivity.Id);
        job.JobDataMap.Add(nameof(candidate.ResourceCenter1CId), candidate.ResourceCenter1CId);
        job.JobDataMap.Add(nameof(candidate.Mvz), candidate.Mvz);
        job.JobDataMap.Add(nameof(candidate.UnitId), candidate.UnitId);
    }
}