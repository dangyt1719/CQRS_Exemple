namespace Entities
{
    public class HrDirectory
    {
        public int Id { get; set; }
        public string MvzId { get; set; }
        public Guid RegionCenterId { get; set; }
        public string HRDirectorIdentifier { get; set; }
        public string HRMvzIdentifier { get; set; }
        public string HRRegionCenterIdentifier { get; set; }
        public string Notifying { get; set; }
    }
}