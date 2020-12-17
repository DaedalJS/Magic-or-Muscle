using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;


namespace TextBasedGame
{
    class Program
    {
        
       
        static void Main(string[] args)
        {

            #region Title Screen, Play / Quit
            Random rnd = new Random();
            bool playing = true;
            
            



            Console.WriteLine("title here");
            Console.WriteLine("press enter to begin or 'quit' to exit.");
            string response;
            do
            {
                response = Console.ReadLine().ToLower().Trim();

                if (response != "quit" && response != "" && response != "start" && response != "begin") { Console.WriteLine("Please type 'quit' or 'begin'. "); }

            } while (response != "quit" && response != "" && response != "start" && response != "begin" );

            #endregion



            if (playing)
            {


                #region lists
                List<string> biomes = new List<string>() { "rocky, mountains", "forest, mountains", "forest", "forest clearing", "medow", "cave", "foothills", "desert", "oasis" };

                // these contian options for possible future expansion
                #region partially implemented but does nothing
                List<string> arStyles = new List<string>() { "normal", "river", "canyon" };
                List<string> weathers = new List<string>() { "pleasant", "rain", "electrical storm", "thunder storm", "heat wave", "cold snap", "snow", "hail" };
                #endregion
                #endregion

                #region Player created, Initial Travel created, Area created
                Player player = Factory.NewPlayer(rnd, biomes);
                Travels travels = Factory.MakeTravels(rnd, player, biomes);
                Area area = Factory.MakeArea(rnd, player, arStyles, weathers);


                Console.WriteLine($"Okay then {player.Name}, Get Moving.  Daylights Burning.");
                EntCont();



                Console.Clear();

                #endregion

                #region GamePlay
                do
                {                    
                    Onward(player, travels, rnd, arStyles, weathers);
                                       
                    player.Score = 0+player.Traveled + ((player.KillOr * 500) / player.BeKilled) + (1000 * player.TravelsRun);
                    if (player.HP <= 0) { break; }

                } while (player.Traveled < travels.TownToTownDist);



                #endregion

                #region GameEnded
                Console.Clear();
                player.DisplayStats();
                Console.WriteLine($"SCORE: {player.Score}");
                Console.WriteLine("\nPress enter to end game.");
                Console.ReadLine();
                


                #endregion
            }
        }

        #region Common Methods;
        


        public static void EntCont()
        {
            Console.WriteLine("Press Enter to Continue.");
            Console.ReadLine();
        }

        // flavor text and ideas for updates
        static void NewTravels()
        {
            // mostly flavor text before play begins.
            // set out to the next town
            // options to pick next next camping area, landmark, etc.  longer travels increase initial chance of battle.
            // chance of battle increases incrementally every "step" of the travel,  
            // resetting when you reach your short term goal, lowering 2-4 "steps" after a battle at most down to the initial chance.


            Console.WriteLine("You set out on your journey.");
        }




        // move along in your travels.
        static void Onward(Player player, Travels travels, Random rnd, List<string> arStyles, List<string> weathers)
        {

            //setup for next area and data display.
            player.Traveled += travels.Steplevel;
            if (player.Traveled > travels.BiomeShiftPoint) { player.MyBiome = travels.Biome2; }

            player.DisplayStats();


            BattleRoll(rnd, player);

            Area whereImAt = Factory.MakeArea(rnd, player, arStyles, weathers);
            Enemy whoIFite = Factory.MakeEnemy(rnd, whereImAt);
            Fight TakeEmOut = new Fight(player, whoIFite);

            whereImAt.AreaDesc();
            EntCont();

            if (whereImAt.EnemyCount > 0)
            {
               do 
               { 
                    if (TakeEmOut.DukeItOut(rnd)) { player.KillOr += 1; whereImAt.EnemyCount -= 1; }

               } while ( whereImAt.EnemyCount > 0 );
            }

            if (player.HP > 0)
            {

                Console.Clear();
                player.DisplayStats();
                Console.WriteLine($"you travel onward.\n");
                if (player.HP < player.MaxHP && player.MP > 30) { player.HP += 50; Console.WriteLine("You channel your mana towards your wounds as you travel.\n Your wounds are looking better as a result.\n"); }
                if (player.HP > player.MaxHP) { player.HP = player.MaxHP; }
                EntCont();
            }
            Console.Clear();
        }

        // Random Battle Roll
        public static void BattleRoll(Random rnd, Player player)
        {
            int battleRoll = rnd.Next(1, 100);
            if (battleRoll < player.BattleChance)
            {
                if (player.BattleChance < 100) { player.BattleChance += 10; }
                player.Fight = true;

            }
            else
            {
                player.Fight = false;
                if (player.BattleChance >= 70) { Console.WriteLine("You're sick and tired of not attracting attention and are now noisily moving on.\n"); }
                else { Console.WriteLine("your fatigue has increased, you become slightly less observant of your surroundings.\n"); player.BattleChance += 10; }
            }

        }


        #endregion


    }
}
