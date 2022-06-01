using Entities;

namespace UseCases.Candidates.Dto
{
    public class WorkingTimeNormDto : Entity
    {
        public int? PercentFromSalary { get; set; }
        public string Description { get; set; }
    }
}