using Statuses.API.Interfaces;
using Statuses.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Statuses.API.Statuses
{
    public class LowHealthStatus : IStatus
    {
        public string Name => "Low health";

        public PrimitiveType Primitive => PrimitiveType.Cube;

        public Color Color => new Color(255, 0, 0);

        public CategoryType Category => CategoryType.Negative;
    }
}
