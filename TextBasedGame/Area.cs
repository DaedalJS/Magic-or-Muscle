using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame
{
    class Area : IArea
    {
        

        public Area(string biome, string arStyle, string weather, bool battle, int enemyCount, int lrgFlora, int smFlora, int lrgFauna, int medFauna, int smFauna)
        {
            Biome = biome;
            ArStyle = arStyle;
            Weather = weather;
            Battle = battle;
            EnemyCount = enemyCount;
            LrgFlora = lrgFlora;
            SmFlora = smFlora;
            LrgFauna = lrgFauna;
            MedFauna = medFauna;
            SmFauna = smFauna;
        }

        public string Biome { get; set; }
        public string ArStyle { get; set; }
        public string Weather { get; set; }
        public bool Battle { get; set; }
        public int EnemyCount { get; set; }
        public int LrgFlora { get; set; }
        public int SmFlora { get; set; }
        public int LrgFauna { get; set; }
        public int MedFauna { get; set; }
        public int SmFauna { get; set; }


        public void AreaDesc()
        {
            Console.WriteLine($"THIS IS WHAT THE AREA LOOKS LIKE!  {EnemyCount}");
            
        }

    }
}
