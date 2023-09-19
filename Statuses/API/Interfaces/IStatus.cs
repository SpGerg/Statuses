using Statuses.API.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Statuses.API.Interfaces
{
    public interface IStatus
    {
        string Name { get; }

        PrimitiveType Primitive { get; }

        Color Color { get; }

        CategoryType Category { get; }
    }
}
