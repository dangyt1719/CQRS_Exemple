using MediatR;
namespace UseCases.UpdateChampi.Queries.UpdateChampi
{
    public class UpdateChampiQuery : IRequest<bool>
    {
        public long Numero { get; set; }
        public int ChampiId { get; set; }
        public string ChampiValue { get; set; }
        public int Count { get; set; }
    }
}
