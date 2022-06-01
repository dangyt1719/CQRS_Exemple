using MediatR;
using NeocaseProviderLibrary.Providers;

namespace UseCases.UpdateChampi.Queries.UpdateChampi
{
    public class UpdateChampiHandler : IRequestHandler<UpdateChampiQuery, bool>
    {
        private readonly NeocaseRootProvider _neocase;

        public UpdateChampiHandler(NeocaseRootProvider neocase)
        {
            _neocase = neocase;
        }
        public async Task<bool> Handle(UpdateChampiQuery request, CancellationToken cancellationToken)
        {
            bool result = await _neocase.NeocaseCaseProvider.UpdateChampiM(request.Numero, request.ChampiId, request.ChampiValue, request.Count);
            return result;
        }
    }
}


