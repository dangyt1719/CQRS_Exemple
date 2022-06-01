namespace Entities
{
    public class RegionCenter
    {
        private string Id { get; set; }

        public Guid Guid
        {
            get => new Guid(Id);
            set => Id = value.ToString();
        }

        public string Title { get; set; }
        public string Location { get; set; }
    }
}