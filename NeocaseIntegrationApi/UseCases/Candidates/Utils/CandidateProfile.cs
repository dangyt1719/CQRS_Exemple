using AutoMapper;
using Entities;
using UseCases.Candidates.Dto;

namespace UseCases.Candidates.Utils
{
    public class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDto>();
            CreateMap<Position, PositionDto>();
            CreateMap<Grade, GradeDto>();
            CreateMap<Region, RegionDto>();
            CreateMap<LegalPerson, LegalPersonDto>();
            CreateMap<LegalPersonPart, LegalPersonPartDto>();
            CreateMap<ProbationPeriod, ProbationPeriodDto>();
            CreateMap<ContractType, ContractTypeDto>();
            CreateMap<WorkFunction, WorkFunctionDto>();
            CreateMap<WorkingTimeNorm, WorkingTimeNormDto>();
            CreateMap<StaffType, StaffTypeDto>();
            CreateMap<Specialization, SpecializationDto>();
            CreateMap<EmployeeActivity, EmployeeActivityDto>();
            CreateMap<EmploymentType, EmploymentTypeDto>();
        }
    }
}