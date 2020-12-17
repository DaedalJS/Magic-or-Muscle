using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame
{
    interface IArea
    {
        string Biome { get; set; }
        string ArStyle { get; set; }
        string Weather { get; set; }
        int EnemyCount { get; set; }
        int LrgFlora { get; set; }
        int SmFlora { get; set; }
        int LrgFauna { get; set; }
        int MedFauna { get; set; }
        int SmFauna { get; set; }
    }
}
