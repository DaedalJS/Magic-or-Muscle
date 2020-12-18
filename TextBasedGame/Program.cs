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
            bool playing = false;
            
            



            Console.WriteLine("Magic or Muscle?");

            Console.WriteLine("press enter to begin or 'quit' to exit.");
            string response;
            do
            {
                response = Console.ReadLine().ToLower().Trim();

                if (response != "quit" && response != "" && response != "start" && response != "begin") { Console.WriteLine("Please type 'quit' or 'begin'. "); }

            } while (response != "quit" && response != "" && response != "start" && response != "begin" );

            #endregion



            do
            {


                #region lists
                List<string> biomes = new List<string>() { "rocky, mountains", "forest, mountains", "forest", "forest clearing", "medow", "cave", "foothills", "desert", "oasis" };

                // these contian options for possible future expansion
                #region partially implemented but does nothing
                List<string> arStyles = new List<string>() { "normal", "river", "canyon" };
                List<string> weathers = new List<string>() { "pleasant", "rainy", "heavy with lightning", "thunderous and stormy", "into swealtering heat", "fridgid", "snowy", "a hailstorm", "windy"};
                #endregion
                #endregion

                #region Player created, Initial Travel created, Area created
                Player player = Factory.NewPlayer(rnd, biomes);
                Travels travels = Factory.MakeTravels(rnd, player, biomes);
                Area area = Factory.MakeArea(rnd, player, arStyles, weathers);
                int travelsDist = 0;
                bool keepGoing = false;


                Console.WriteLine($"Okay then {player.Name}, Get Moving.  Daylights Burning.");
                EntCont();



                Console.Clear();

                #endregion

                #region GamePlay
                do
                {
                    // put travels flavor text and description here.

                    do
                    {
                        Onward(player, travels, rnd, arStyles, weathers);

                        player.Score = 0 + player.Traveled + player.PreviousTravels + ((player.KillOr * 500) / player.BeKilled) + (1000 * player.TravelsRun);
                        if (player.HP <= 0) { break; }

                    } while (player.Traveled < travels.TownToTownDist && player.HP > 0);

                    if (player.HP > 0)
                    {
                        player.PreviousTravels += travels.TownToTownDist;

                        Console.WriteLine("you reach the next town and book a room at an inn.\n" +
                            "after a good nights rest and a hearty meal or two you ponder if you should keep going.\n" +
                            "Keep Going?\n \n" +
                            "1. Yes\n2. No");


                        do
                        {
                            response = Console.ReadLine().ToLower().Trim();

                            if (response != "yes" && response != "no" && response != "1" && response != "2") { Console.WriteLine("Please type 'yes', 'no', '1', or '2'. "); }

                        } while (response != "yes" && response != "no" && response != "1" && response != "2");
                        if (response == "yes" || response == "1") { keepGoing = true; Console.WriteLine("You decide to continue on.\n"); player.Traveled = 0; } else { keepGoing = false; Console.WriteLine("You decide this is a good place to stop."); }
                        
                        EntCont();
                        travels = Factory.MakeTravels(rnd, player, biomes);

                    }
                } while (keepGoing && player.HP > 0);

                #endregion

                #region GameEnded
                Console.Clear();
                player.DisplayStats();
                Console.WriteLine($"SCORE: {player.Score}");
                Console.WriteLine("\nPlay Again?");
                Console.WriteLine("\n1.Yes\n2.No\n");
                do
                {
                    response = Console.ReadLine().ToLower().Trim();

                    if (response != "yes" && response != "no" && response != "1" && response != "2") { Console.WriteLine("Please type 'yes', 'no', '1', or '2'. "); }

                } while (response != "yes" && response != "no" && response != "1" && response != "2");
                if (response == "yes" || response == "1") { playing = true; } else { playing = false; }

                #endregion

            } while (playing) ;

                Console.WriteLine("\nPress enter to end game.");
                Console.ReadLine();

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

            whereImAt.AreaDesc(player);
            EntCont();

            if (whereImAt.EnemyCount > 0 && player.HP > 0)
            {
               do 
               { 
                    if (TakeEmOut.DukeItOut(rnd)) { player.KillOr += 1; whereImAt.EnemyCount -= 1; }

                    if (whereImAt.EnemyCount == 0) 
                    {
                        player.BattleChance -= 20; 
                        if (player.BattleChance < 10) { player.BattleChance = 10; } 
                    }
                    if (player.HP <1) { break; }
               } while ( whereImAt.EnemyCount > 0 && player.HP > 0);
            }

            if (player.HP > 0)
            {

                Console.Clear();
                player.DisplayStats();
                Console.WriteLine($"you travel onward.\n");

                if (player.MP > 30)
                {
                    if (player.HP < player.MaxHP) { Console.WriteLine("You channel your mana towards your wounds as you travel.\n Your wounds are looking better as a result.\n"); }
                    if (player.HP < (player.MaxHP - 30)) { player.HP += 30; player.MP -= 30; } else { player.HP += 30; player.MP -= 20; }
                    if (player.HP > player.MaxHP) { player.HP = player.MaxHP; }
                }
                player.Stamina += 30; player.MP += 30;
                if (player.MP > player.MaxMP) { player.MP = player.MaxMP; }
                if (player.Stamina > player.MaxStamina) { player.Stamina = player.MaxStamina; }
                
                EntCont();
            }
            Console.Clear();
        }

        // Random Battle Roll
        public static void BattleRoll(Random rnd, Player player)
        {
            if (player.HP > 0)
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
                    else { Console.WriteLine("as your travels continue you worry less about potential dangers. \n"); player.BattleChance += 10; }
                }

            }
        }


        #endregion


    }
}
