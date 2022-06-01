using Infrastructure.Implementation.Repositories;
using Infrastructure.Interfaces.RepositoryInterfaces;
using MediatR;
using NeocaseProviderLibrary.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.SetHrRcPartner.Commands.SetHrRcPartner
{
    public class SetHrRcPartnerHandler : IRequestHandler<SetHrRcPartnerCommand, bool>
    {
        private readonly NeocaseRootProvider _neocase;
        private readonly IHrDirectoryRepository _hrDirectoryRepository;

        public SetHrRcPartnerHandler(NeocaseRootProvider neocase, IHrDirectoryRepository hrDirectoryRepository)
        {
            _neocase = neocase;
            _hrDirectoryRepository= hrDirectoryRepository;
        }
        public async Task<bool> Handle(SetHrRcPartnerCommand request, CancellationToken cancellationToken)
        {
            var caseMvzRc = await _neocase.NeocaseDbProvider.GetCaseByIdAsync(request.Numero);
            if (caseMvzRc!=null)
            {
                var Pernr = await _hrDirectoryRepository.GetHrDirectoryByRCMvz(string.IsNullOrEmpty(caseMvzRc.Rc) ? new Guid() : new Guid(caseMvzRc.Rc), request.MVZ);
                bool result = await _neocase.NeocaseCaseProvider.UpdateChampi((long)request.Numero, 803, Pernr?.HRRegionCenterIdentifier);
                return result;
            }
            return false;
        }
    }
}
