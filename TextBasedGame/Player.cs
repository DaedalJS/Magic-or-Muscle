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
            MaxHP = 100;
            HP = 100;
            MaxMP = 100; 
            MP = 100;
            Stamina = 100;
            MaxStamina = 100;
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
            Score = 0;
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
                string response;
            do
            {
                Console.WriteLine("Choose an action.");
                Console.WriteLine("1. Dodge \n2. Magic \n3. Muscle\n");
                do
                {
                    response = Console.ReadLine();
                    response = response.ToLower().Trim();
                    if (response != "1" && response != "2" && response != "3" && response != "dodge" && response != "magic" && response != "muscle")
                    {
                        Console.WriteLine("Please pick a listed response by number or word.\n");
                    }


                } while (response != "1" && response != "2" && response != "3" && response != "dodge" && response != "magic" && response != "muscle");


                switch (response)
                {
                    case "1":
                    case "dodge":
                        Stance = 0;
                        break;
                    case "2":
                    case "magic":
                        if (MP > 39)
                        {
                            Stance = 4;
                            MagUses += 1;
                        }
                        else { response = "7"; Console.WriteLine("Not enough MP!\n"); }
                        break;
                    case "3":
                    case "muscle":
                        if (Stamina > 39)
                        {
                            Stance = 1;
                            AtkUses += 1;
                        }
                        else { response = "7"; Console.WriteLine("Not enough Stamina!\n"); }
                        break;
                    default:
                        Stance = 1;
                        break;
                }


            }
            while (response == "7");
            if (Stance != 0)
            {
                Console.WriteLine("\nPick an action!");

                                
                    Console.WriteLine("\n1. Attack \n2. Defend \n3. Counter\n");
                    do
                    {
                        response = Console.ReadLine();
                        response = response.ToLower().Trim();
                        if (response != "1" && response != "2" && response != "3" && response != "attack" && response != "defend" && response != "counter")
                        {
                            Console.WriteLine("Please pick a listed response by number or word.");
                        }


                    } while (response != "1" && response != "2" && response != "3" && response != "attack" && response != "defend" && response != "counter");
                
                 
            }
                switch (response)
                {
                    case "1":
                    case "attack":
                        
                        Stance += 0;
                        

                        break;
                    case "2":
                    case "defend":
                    Stance += 1;
                    

                        break;
                    case "3":
                    case "counter":
                    Stance += 2;
                    
                    break;
                    default:
                        break;
                }
            
        }

        public void DisplayStats()
        {
            Console.WriteLine($"{Name} HEALTH:{HP} / {MaxHP} | MANA: {MP} / {MaxMP} | STAMINA: {Stamina} / {MaxStamina}");
            Console.WriteLine($"Score: {Score} | Traveled:{Traveled} malms | Batttle Wins: {KillOr}\n");
            
        }
        public void UpdateStats()
        {
            
            MaxHP = 100 + (AtkUses / 10) + (KillOr / 3) + (TravelsRun * 10);
            if (MaxHP > 500) { MaxHP = 500; }
            MaxMP = 100 + (MagUses / 10);
            if (MaxMP > 150) { MaxMP = 150; }
            MaxStamina = 100 + (AtkUses / 10);
            if (MaxStamina > 150) { MaxStamina = 150; }
        }
    }
}
