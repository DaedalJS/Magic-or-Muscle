using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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


        public void AreaDesc(Player player)
        {
            Console.WriteLine($"as you walk through the {Biome}, the weather turns {Weather}. \n"); 
            Thread.Sleep(500);
            if (LrgFauna > MedFauna && LrgFauna > SmFauna) { Console.WriteLine("You see a fair amount of large game here.\n"); }
            else
            {
                if (MedFauna > SmFauna) { Console.WriteLine("the animal population seems to be mostly medium sized here.\n"); }
                else
                {
                    Console.WriteLine("most the fauna here are small. you'd need several of them for a decent meal.\n");
                }
            } 
            Thread.Sleep(500);
            if (SmFlora > 4) { Console.WriteLine("You can't help but notice there's plenty to forage from here.\n"); }
            else if (SmFlora > 2) { Console.WriteLine("there's just enough edible plants growing here to forage from.\n"); }
            else { Console.WriteLine("looking around you don't see anything edible growing\n"); }
            Thread.Sleep(500);
            if (EnemyCount > 0) { Console.WriteLine($"Among your observations, you notice that there's a fight waiting here.  Enemies: {EnemyCount}");


                switch (player.Dodge)
                {
                    case 60:
                        Console.WriteLine($"Thankfully the thick vegitation will make your dodges easier.");
                        break;
                    case 55:
                        Console.WriteLine($"At least there's more than the average amount to dodge behind.");
                        break;
                    case 50:
                        Console.WriteLine($"You realize there's an average amount to dodge behind.");
                        break;
                    case 45:
                        Console.WriteLine($"Just your rotten luck, there's not much to dodge behind.");
                        break;
                    case 40:
                        Console.WriteLine($"Unfortunately there's nothing to dodge behind...");
                        break;

                }
            }
            
        }

    }
}
