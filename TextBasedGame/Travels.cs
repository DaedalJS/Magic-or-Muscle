using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame
{
    public class Travels : ITravels
    {
       

        public int TownToTownDist { get; set; }
        public int Steplevel { get; set; }
        public int SegmentsRemaining { get; set; }
        public string Biome1 { get; set; }
        public int BiomeShiftPoint { get; set; }
        public string Biome2 { get; set; }

        
        public Travels(int totalDist, int segments, int sl, string thisBiome, string nextBiome, int biomeShiftPoint, Random rnd)
        {
            TownToTownDist = totalDist;
            Steplevel = sl;
            Biome1 = thisBiome;
            Biome2 = nextBiome;            
            BiomeShiftPoint = biomeShiftPoint;
            SegmentsRemaining = segments;




        }


    }
}
