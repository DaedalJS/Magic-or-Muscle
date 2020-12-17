using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame
{
    public class Player
    {
        public Player(string name, string biome)
        {

            Name = name;
            MyBiome = biome;
            MaxHP = 100 + (TravelsRun*10)+(KillOr/10);
            HP = 100;
            MaxMP = 100 + (MagUses/10);
            MP = 100;
            Stamina = 100;
            MaxStamina = 100 + (AtkUses/10);
            Dodge = 40;
            Stance = 0;
            MagUses = 0;
            AtkUses = 0;
            BattleChance = 10;
            Traveled = 0;
            Reviving = false;
            KillOr = 0;
            BeKilled = 1;
            TravelsRun = 0;
            Score = 0 +Traveled + ((KillOr * 500) / BeKilled) + (1000 * TravelsRun);
            Fight = false;

        }

        #region Properties
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int HP { get; set; }
        public int MaxMP { get; set; }
        public int MP { get; set; }
        public int MagUses { get; set; }
        public int MaxStamina { get; set; }
        public int Stamina { get; set; }
        public int AtkUses { get; set; }
        public int BattleChance { get; set; }
        public int Traveled { get; set; }
        public bool Reviving { get; set; }
        public int KillOr { get; set; }
        public int BeKilled { get; set; }
        public int Score { get; set; }
        public string MyBiome { get; set; }
        public int TravelsRun { get; set; }
        public int Dodge { get; set; }
        public int Stance { get; set; }
        public bool Fight { get; set; }
        #endregion

        // this sets the player stance in a battle.
        public void BattleOpt()
        {
            Stance = 0;
            Console.WriteLine("Choose an action.");
            Console.WriteLine("1. Dodge \n2. Attack \n3. Defend \n4. Counter");
                string response;
            do
            {
                response = Console.ReadLine();
                response = response.ToLower().Trim();
                if (response != "1" && response != "2" && response != "3" && response != "4" && response != "dodge" && response != "attack" && response != "defend" && response != "counter")
                {
                    Console.WriteLine("Please pick a listed response by number or word.");
                }


            } while (response != "1" && response != "2" && response != "3" && response != "4" && response != "dodge" && response != "attack" && response != "defend" && response != "counter");
            
            switch (response) 
            {
                case "1":
                case "dodge":
                    Stance = 0;
                    break;
                case "2":
                case "attack":
                    Stance = 1;
                    break;
                case "3":
                case "defend":
                    Stance = 2;
                    break;
                case "4":
                case "counter":
                    Stance = 3;
                    break;
                default:
                    Stance = 1;
                    break;
            }

            if (Stance != 0) 
            {
                Console.WriteLine("Pick a Method.");
                Console.WriteLine("1. Magic \n2. Muscle");
                do
                {
                    response = Console.ReadLine();
                    response = response.ToLower().Trim();
                    if (response != "1" && response != "2" && response != "Muscle" && response != "Magic")
                    {
                        Console.WriteLine("Please pick a listed response by number or word.");
                    }


                } while (response != "1" && response != "2" && response != "muscle" && response != "magic");

                switch (response)
                {
                    case "1":
                    case "magic":
                        Stance = Stance + 3;
                        break;
                    case "2":
                    case "muscle":
                        
                        break;
                    default:
                        break;
                }
            }

        }

        public void DisplayStats()
        {
            Console.WriteLine($"{Name} HEALTH:{HP} / {MaxHP} | MANA: {MP} / {MaxMP} | STAMINA: {Stamina} / {MaxStamina}");
            Console.WriteLine($"Score: {Score} | Traveled:{Traveled} malms | Batttle Wins: {KillOr}");
            Console.WriteLine("\n");

        }


    }
}
