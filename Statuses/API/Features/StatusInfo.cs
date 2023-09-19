using Exiled.API.Features;
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
        public Player Owner { get; }

        public IStatus Status { get; }

        public Primitive Primitive { get; }

        public StatusInfo(Player owner, IStatus status, Primitive primitive)
        {
            Owner = owner;
            Status = status;
            Primitive = primitive;
        }
    }
}
