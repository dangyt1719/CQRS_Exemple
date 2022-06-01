using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.SetHrRcPartner.Commands.SetHrRcPartner
{
    public class SetHrRcPartnerCommand : IRequest<bool>
    {
        public long Numero { get; set; }
        public string MVZ { get; set; }
    }
}
