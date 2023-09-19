using Statuses.API.Enums;
using Statuses.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Statuses.API.Statuses
{
    public class SomePositiveEffectStatus : IStatus
    {
        public string Name => "Positive effect";

        public PrimitiveType Primitive => PrimitiveType.Cube;

        public Color Color => new Color(0, 255, 0);

        public CategoryType Category => CategoryType.Positive;
    }
}
