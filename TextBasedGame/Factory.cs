using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame 
{


    static class Factory
    {
            

        // this generates the player object used during the game.
        #region Player Creator

        public static Player NewPlayer(Random rng, List<string> biomes)
        {
            Console.WriteLine("What is your name?");
            string name;
            do
            {
                name = Console.ReadLine();

                if (name == "") { Console.WriteLine("Please Write a Name."); }
            } while (name == "");
            int rando = rng.Next(0, 8);
            string biome = biomes[rando];            
            return new Player(name, biome);
        }

        #endregion


        // FINISH ENEMY CREATOR
        #region Enemy Creator
        public static Enemy MakeEnemy(Random rng, Area area)
        {
            List<string> EnemySpecies = new List<string>() { "Bandit", "Goblin", "Orc", "Hobgoblin", "Ogre", "Ghoul" };
            int type = rng.Next(1, 3);
            string name = EnemySpecies[rng.Next(0, 5)];
            int hp = rng.Next(20, 150);
            int mp = rng.Next(20, 150);
            int number = area.EnemyCount;
            return new Enemy(number,name,hp,mp,type);
        }
        #endregion


        // generates the Travel to the next town.
        #region Travel Creator
        public static Travels MakeTravels(Random rng, Player player, List<string> biomes)
        {
            int tttd = rng.Next(400, 1200);
           
            int segs = (tttd / 400) * 2;
            int sl = tttd / rng.Next(7, 13);
            string b1 = player.MyBiome;
            int bsp = rng.Next(tttd / 3, (tttd / 3) * 2);
            int rando = rng.Next(0, 8);
            string b2 = biomes[rando];

            


            return new Travels(tttd, segs, sl, b1, b2, bsp, rng);
        }
        #endregion


        // generates an Area object.
        #region Area Creator
        public static Area MakeArea(Random rnd, Player player, List<string> arStyles, List<string> weathers)
        {
            string biome = player.MyBiome;
            int rndnum = rnd.Next(0, 2);
            string arStyle = arStyles[rndnum];
            rndnum = rnd.Next(0, 8);
            string weather = weathers[rndnum];
            bool battle = player.Fight;
            int enemyCount;
            if (battle)
            {
                if (player.TravelsRun > 1) { enemyCount = rnd.Next(1, 1 + player.TravelsRun); } else { enemyCount = 1; }
            } else { enemyCount = 0; }

            int floraMaxLim = 0;
            int floraMinLim = 0;

            int faunaMaxLim = 0;
            int faunaMinLim = 0;
            bool weightedBiome = true;

            #region settings switch for random env population
            // biome settings for somewhat random population of environment
            switch (player.MyBiome)
            {
                case "forest, mountains":
                    floraMaxLim = 40;
                    floraMinLim = 20;
                    faunaMaxLim = 20;
                    faunaMinLim = 10;
                    weightedBiome = true;
                    break;
                case "forest":
                    floraMaxLim = 40;
                    floraMinLim = 20;
                    faunaMaxLim = 20;
                    faunaMinLim = 10;
                    weightedBiome = true;
                    break;
                case "forest clearing":
                    floraMaxLim = 20;
                    floraMinLim = 10;
                    faunaMaxLim = 20;
                    faunaMinLim = 10;
                    weightedBiome = true;
                    break;
                case "foothills":
                    floraMaxLim = 20;
                    floraMinLim = 10;
                    faunaMaxLim = 20;
                    faunaMinLim = 10;
                    weightedBiome = true;
                    break;
                case "medow":
                    floraMaxLim = 20;
                    floraMinLim = 10;
                    faunaMaxLim = 20;
                    faunaMinLim = 10;
                    weightedBiome = true;
                    break;
                case "cave":
                    floraMaxLim = 15;
                    floraMinLim = 5;
                    faunaMaxLim = 10;
                    faunaMinLim = 0;
                    weightedBiome = false;
                    break;
                case "oasis":
                    floraMaxLim = 15;
                    floraMinLim = 5;
                    faunaMaxLim = 10;
                    faunaMinLim = 0;
                    weightedBiome = false;
                    break;
                case "rocky, mountains":
                    floraMaxLim = 15;
                    floraMinLim = 5;
                    faunaMaxLim = 15;
                    faunaMinLim = 5;
                    weightedBiome = false;
                    break;
                case "desert":
                    floraMaxLim = 15;
                    floraMinLim = 5;
                    faunaMaxLim = 10;
                    faunaMinLim = 0;
                    weightedBiome = false;
                    break;
                default:
                    floraMaxLim = 15;
                    floraMinLim = 5;
                    faunaMaxLim = 10;
                    faunaMinLim = 0;
                    weightedBiome = false;
                    break;

            }
            
            #endregion

            #region Random plant population
            // somewhat random population of plants
            int lrgFlora;
            int smFlora;
            int weightedCount;

            int totalFlora = rnd.Next(floraMinLim, floraMaxLim);
            if (weightedBiome)
            {
                weightedCount = totalFlora - (totalFlora / 3);
                rndnum = rnd.Next(weightedCount, totalFlora);
            }
            else 
            { rndnum = rnd.Next(floraMinLim, totalFlora); }
            lrgFlora = rndnum;
            smFlora = totalFlora - lrgFlora;
            #endregion           

            #region Random animal population
            // somewhat random population of animals
            int lrgFauna;
            int medFauna;
            int smFauna;
            
            int totalFauna = rnd.Next(faunaMinLim, faunaMaxLim);
            if (weightedBiome)
            {
                weightedCount = totalFauna / 3;
                lrgFauna = rnd.Next(1, weightedCount);
                medFauna = rnd.Next(1, totalFauna-lrgFauna);
                smFauna = rnd.Next(0, totalFauna - lrgFauna - medFauna);
            }
            else
            { 
                lrgFauna = rnd.Next(0, totalFauna);
                medFauna = rnd.Next(0, (totalFauna - lrgFauna));
                smFauna = rnd.Next(0, (totalFauna - lrgFauna - medFauna));
            }

            if (enemyCount > 3) { lrgFauna = 0; }

            #endregion
           


            // area instance made here
            Area thisArea = new Area(biome, arStyle, weather, battle, enemyCount, lrgFlora, smFlora, lrgFauna, medFauna, smFauna);




            //more trees means more things to dodge behind
            if (battle)
            {
                if (thisArea.LrgFlora > 20) { player.Dodge = 60; }
                else if (thisArea.LrgFlora > 15) { player.Dodge = 55; }
                else if (thisArea.LrgFlora > 10) { player.Dodge = 50; }
                else if (thisArea.LrgFlora > 5) { player.Dodge = 45; }
                else { player.Dodge = 40; }
            }
            return thisArea;
        }
        #endregion

      
    }
}
