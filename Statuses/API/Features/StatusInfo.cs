using Exiled.API.Features.Toys;
using Statuses.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statuses.API.Features
{
    public class StatusInfo
    {
        public IStatus Status { get; }

        public Primitive Primitive { get; }

        public StatusInfo(IStatus status, Primitive primitive)
        {
            Status = status;
            Primitive = primitive;
        }
    }
}
