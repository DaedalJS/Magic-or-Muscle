using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame
{
    interface ITravels
    {
        public int TownToTownDist { get; set; }
        public int SegmentsRemaining { get; set; }
        public string Biome1 { get; set; }
        public int BiomeShiftPoint { get; set; }
        public string Biome2 { get; set; }
    }
}
