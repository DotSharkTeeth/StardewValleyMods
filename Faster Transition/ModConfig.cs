using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FasterTransition
{
    public sealed class ModConfig
    {
        public bool Enable { get; set; } = true;
        public bool NoTransition { get; set; } = false;
        public float Speed { get; set; } = 0.003f;
    }
}
