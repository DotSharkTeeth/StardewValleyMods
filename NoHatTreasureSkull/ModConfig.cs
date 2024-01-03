using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoHatTreasureSkull
{
    public sealed class ModConfig
    {
        public bool Enable { get; set; } = true;
        public bool EnableMachine { get; set; } = true;
        public bool EnableSeed { get; set; } = false;
        public bool EnableBomb { get; set; } = false;
        public bool EnableMedicine { get; set; } = false;
        public bool EnableSapling { get; set; } = false;
    }
}
