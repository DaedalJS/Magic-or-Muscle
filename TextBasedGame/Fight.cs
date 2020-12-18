using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TextBasedGame
{
    class Fight
    {
        public Fight(Player protag, Enemy antag)
        {
            FightName = $"{antag.Name}+{antag.Number}";
            Protag = protag;
            Antag = antag;
            Won = false;
            Lost = false;
        }

        public string FightName { get; set; }
        public Player Protag { get; set; }
        public Enemy Antag { get; set; }
        public bool Won { get; set; }
        public bool Lost { get; set; }

        public bool DukeItOut(Random rnd)
        { 
            var spells = new List<string>() { "fireball", "windblade", "arcane bolt", "lightning", "ice spike", "expecto patronum", "magic missile", "rasengan", "summoned fist"};
            var colors = new List<string>() { "blue", "green", "red", "yellow", "violet", "brown", "orange", "black", "white" };
            var descriptors1 = new List<string>() { "mean", "angry", "dour", "awkward", "merry", "frightening", "upsetting" };
            var descriptors2 = new List<string>() {"angrily","snidely","merrily","rudely","vulgarly","awkwardly","poorly","oddly" };
            var expressions = new List<string>() {"sneer","glower","stare","grin","smirk","frown","laugh" };
            var jeers = new List<string>() { "taunts", "mocks", "spits", "shouts", "grunts", "farts","mumbles" };
            var weapons = new List<string>() { "club", "dirk", "sword", "axe", "staff", "knife", "mace", "halberd", "spear", "fists" };

            string color = colors[rnd.Next(0,8)];   
            string color2 = colors[rnd.Next(0, 8)];
            string descriptive1 = descriptors1[rnd.Next(0, 6)];
            string expression = expressions[rnd.Next(0,6)];
            string mocks = jeers[rnd.Next(0, 6)];
            string descriptive2 = descriptors2[rnd.Next(0, 7)];
            string weapon = weapons[rnd.Next(0, 9)];

            Console.Clear();
            Protag.DisplayStats();
            Console.WriteLine($"Before you stands a {Antag.Name} with {color} eyes, {color2} hair, and a {descriptive1} {expression}.\n " +
                $"it {mocks} you {descriptive2} before it raises it's {weapon} in preparation for the fight.\n");

            Program.EntCont();
            do
            {
                if (Protag.HP < 1) { break; }
                Console.Clear();
                Protag.DisplayStats();
                Antag.DisplayStats();

                Protag.BattleOpt();
                Antag.EnemyFightChoice(rnd);
                string spell = spells[rnd.Next(0, 8)];
                string spell2 = spells[rnd.Next(0, 8)];

                // flavor text and stat changes for user options.
                switch (Protag.Stance)
                {
                    case 0:
                        Console.WriteLine("\nYou ready yourself to dodge. \n");
                        break;
                    case 1:
                        Console.WriteLine($"\nYou swiftly pull your dagger and lunge towards the {Antag.Name}\n");
                        Protag.Stamina -= 40; Protag.MP += 15; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                        break;
                    case 2:
                        Console.WriteLine("\nIn anticipation of an attack you throw your guard up.\n");
                        Protag.Stamina -= 40; Protag.MP += 15; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }

                        break;
                    case 3:
                        Console.WriteLine($"\nYou wait patiently yet prepared for the {Antag.Name} to strike.\n");
                        Protag.Stamina -= 40; Protag.MP += 15; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }

                        break;
                    case 4:                        
                        Console.WriteLine($"you quickly spell cast {spell} at the {Antag.Name}.\n");
                        Protag.MP -= 40; Protag.Stamina += 15; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }

                        break;
                    case 5:
                        Console.WriteLine($"You cast a magic shield to dissipate the force of magic attacks.\n");
                        Protag.MP -= 40; Protag.Stamina += 15; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                        break;
                    case 6:
                        Console.WriteLine("expecting a magic attack, you prepare a reflect spell.\n");
                        Protag.MP -= 40; Protag.Stamina += 15; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                        break;
                    default:
                        Console.WriteLine($"\nYou and the {Antag.Name} are not sure what you're looking at but it doesn't seem right.\n");
                        Protag.Stance = rnd.Next(0, 6); Antag.Stance = rnd.Next(1, 6);
                        break;
                }

                Thread.Sleep(500);
                int rolls;                
                // user tries to dodge
                if (Protag.Stance == 0) 
                {
                    rolls = rnd.Next(1, 100);
                    if (Protag.Dodge <= rolls) 
                    {
                        Console.WriteLine("You dodge the attack"); 
                        Protag.MP += 50; 
                        Protag.Stamina += 50; 
                        if (Protag.MaxMP < Protag.MP) { Protag.MP = Protag.MaxMP; }
                        if (Protag.MaxStamina < Protag.Stamina) { Protag.Stamina = Protag.MaxStamina; }

                    }
                    else 
                    {
                        Console.WriteLine("Your moves were predicted and you took a hit.");
                        Protag.HP -= 20;
                        Protag.MP += 25;
                        Protag.Stamina += 25;
                        if (Protag.MaxMP < Protag.MP) { Protag.MP = Protag.MaxMP; }
                        if (Protag.MaxStamina < Protag.Stamina) { Protag.Stamina = Protag.MaxStamina; }
                    }

                        Program.EntCont();



                }

                // user attacks with muscle
                if (Protag.Stance == 1)
                {
                    switch (Antag.Stance)
                    {
                        
                        case 1:
                            Console.WriteLine($"\nThe {Antag.Name} attacks at the same time. you both land clean hits. \n");
                            Protag.HP -= 20; Antag.HP -= 20;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 2:
                            Console.WriteLine($"\nGuard ready against your attack, the {Antag.Name} deflects it without much effort. \n");
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 3:
                            Console.WriteLine($"\nPrepared and waiting for it, the {Antag.Name} {descriptive2} counters your attack. \n");
                            Protag.HP -= 20;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 4:                            
                            Console.WriteLine($"\nThe Attack lands just when the {Antag.Name} unleashes a {spell2} at you. You gave as good as you got.\n");
                            Protag.HP -= 20; Antag.HP -= 20;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();

                            break;
                        case 5:
                            Console.WriteLine($"\nThe {Antag.Name}'s magic shiled robs a bit of your momentum but you break through landing a weakened hit. \n");
                            Antag.HP -= 10;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();

                            break;
                        case 6:
                            Console.WriteLine($"\nYou land a viciuos blow on the {Antag.Name}. It was clearly expecting something else.\n");
                            Antag.HP -= 40;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        default:
                            Console.WriteLine($"\nThe battle devolves into a grasping clawing roll in the dirt. \n" +
                                $"it's clear you're evenly matched but it's also clear you both suck at this. \n");
                            Protag.HP -= 5; Antag.HP -= 5;
                            Program.EntCont();
                            break;
                    }
                }


                // user defends with muscle
                if (Protag.Stance == 2)
                {
                    switch (Antag.Stance)
                    {

                        case 1:
                            Console.WriteLine($"\nYou easily parry the {Antag.Name}'s attack.\n");
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 2:
                            Console.WriteLine($"\nYou both threw your guards up and you both almost immediately realize at least one of you needs to do something else.\n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 3:
                            Console.WriteLine($"\nAfter the {Antag.Name} sees no attack to counter he simply taunts you {descriptive2}. \n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 4:
                            Console.WriteLine($"\nYour parry stopped a direct hit from the {Antag.Name}'s {spell2}.\n" +
                                $" Still, the stinging pain tells you its better to block magic with magic or dodge it completely.  \n");
                            Protag.HP -= 10;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 5:
                            Console.WriteLine($"\nThe {Antag.Name} casts a magic shield.\n" +
                                $"Realizing there's no spell incoming it mutters something {descriptive2} to itself. \n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 6:
                            Console.WriteLine($"\nThrough your guard you see the {Antag.Name} briefly enact the powers of it's reflection talisman.\n" +
                                $"well, this exchange has been less eventful than expected all around it seems.\n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        default:
                            Console.WriteLine($"\nThe battle devolves into a grasping clawing roll in the dirt. \n" +
                                $"it's clear you're evenly matched but it's also clear you both suck at this. \n");
                            Protag.HP -= 5; Antag.HP -= 5;
                            Program.EntCont();
                            break;
                    }
                }

                // user counters with muscle
                if (Protag.Stance == 3)
                {
                    switch (Antag.Stance)
                    {

                        case 1:
                            Console.WriteLine($"\nYou deftly sidestep the incoming attack and land a nasty blow on the {Antag.Name}'s side. \n");
                            Antag.HP -= 40;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 2:
                            Console.WriteLine($"\nYou wait patiently throwing taunts at the {Antag.Name} in the hopes it'll try to attack.  " +
                                $"\nIt reacts defensively and insults you {descriptive2} in return\n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 3:
                            Console.WriteLine($"\nYou both start taunting and insulting each other to draw an attack.\n" +
                                $"yet the only thing you give each other to counter are taunts and insults.\n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 4:                            
                            Console.WriteLine($"\nYou avoid the {Antag.Name}'s {spell2} but are left without a chance to counter.\n");
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 5:
                            Console.WriteLine($"\n You taunt and insult the {Antag.Name} trying to get it to attack...\n" +
                                $"but you doubt it heard any of it over the hum of the magic shield it was casting. \n");
                            Protag.Stamina += 5;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        case 6:
                            Console.WriteLine($"\nAs you both stare each other down while waiting for the other to make a move.\n" +
                                $"you both slowly realize this course of action is going nowhere\n");
                            Antag.HP -= 40;
                            Protag.MP += 10; if (Protag.MP > Protag.MaxMP) { Protag.MP = Protag.MaxMP; }
                            Program.EntCont();
                            break;
                        default:
                            Console.WriteLine($"\nThe battle devolves into a grasping clawing roll in the dirt. \n" +
                                $"it's clear you're evenly matched but it's also clear you both suck at this. \n");
                            Protag.HP -= 5; Antag.HP -= 5; 
                            Program.EntCont();
                            break;
                    }
                }


                // user attacks with magic
                if (Protag.Stance == 4)
                {
                    switch (Antag.Stance)
                    {

                        case 1:
                            Console.WriteLine($"\nYour {spell}, though effective, wasn't enough to deter the {Antag.Name}'s attack.\n" +
                                $"you both take clean hits\n");
                            Protag.HP -= 20; Antag.HP -= 20;
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 2:
                            Console.WriteLine($"\nThe {Antag.Name} tries to parry your {spell}. \n" +
                                $"the the pained expression on it's face lets you know it wasn't entirely effective.\n");
                            Antag.HP -= 10;
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 3:
                            Console.WriteLine($"\nYou cast {spell}. The {Antag.Name}, prepared for a counter, sidesteps it but couldn't position itself to land a hit.\n");
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 4:
                            
                            Console.WriteLine($"\nYou throw {spell}, the {Antag.Name} throws {spell2} \n" +
                                $"Your spells collide exploding in a massive blast of energy.\n" +
                                $"The force of the blast throws you both away.");
                            Protag.HP -= 30; Antag.HP -= 30;
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 5:
                            Console.WriteLine($"\nThe {Antag.Name} throws up a magic shield just in time to dissipate your {spell}. \n");
                            
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 6:
                            Console.WriteLine($"\n Reacting quickly, the {Protag.Name} slams the {spell} with a reflection talisman.\n" +
                                $"the talisman feeds more magic into your {spell}. landing a strong blow. \n");

                            Protag.HP -= 30;
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        default:
                            Console.WriteLine($"\nThe battle devolves into a grasping clawing roll in the dirt. \n" +
                                $"it's clear you're evenly matched but it's also clear you both suck at this. \n");
                            Protag.HP -= 5; Antag.HP -= 5;
                            Protag.Stamina -= 5; if (Protag.Stamina < 0) { Protag.Stamina = 0; }
                            Program.EntCont();
                            break;
                    }
                }

                // user defends with magic
                if (Protag.Stance == 5)
                {
                    switch (Antag.Stance)
                    {

                        case 1:
                            Console.WriteLine($"\nThe {Antag.Name} attacks with a {weapon}. \n" +
                                $"your magic shield shatters and you take a somewhat weakened hit.\n");
                            Protag.HP -= 10; 
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 2:
                            Console.WriteLine($"\nYou both ready separate defenses for attacks that never come. \n");
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }

                            Program.EntCont();
                            break;
                        case 3:
                            Console.WriteLine($"\nYou cast your magic shield. 'FFFFFFFFFFFFFVVVVVVVVVWWWWWWWUUUUUUMMMMMMMMMMMMMMMMM' \n" +
                                $"The {Antag.Name} appears to be shouting at you but you can't tell what it's saying.\n");
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 4:
                            Console.WriteLine($"\nYou cast your magic shield just in time! \n" +
                                $"The {Antag.Name}'s {spell2} breaks harmlessly against it allowing you to absorb a bit of it's mana.");
                                Protag.MP += 10;
                                Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 5:
                            Console.WriteLine($"\nYou both cast magic shields.  \n" +
                                $"the two shields resonate strangely with each other before suddenly shattering harmlessly.  \n");
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 6:
                            int which = rnd.Next(1, 100);
                            string boom;
                            if (which > 50) { boom = "BOOM! the two magic defenses suddenly collapse in towards you!\n"; } else { boom = $"BOOM! the two magic defenses suddenly explode outwards on the {Antag.Name}!\n"; }
                            Console.WriteLine($"\nThe {Antag.Name}'s magic reflect talisman and your magic shield collide. \n" +
                                $"The two magics become unstable and start to build force constantly rebounding.\n" +
                                $"Faster an Louder until suddenly\n" + boom); 
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            if (which > 50) { Protag.HP -= 30; } else { Antag.HP -= 30; }
                            Program.EntCont();
                            break;
                        default:
                            Console.WriteLine($"\nThe battle devolves into a grasping clawing roll in the dirt. \n" +
                                $"it's clear you're evenly matched but it's also clear you both suck at this. \n");
                            Protag.HP -= 5; Antag.HP -= 5;
                            Program.EntCont();
                            break;
                    }
                }

                // user counters with magic
                if (Protag.Stance == 6)
                {
                    switch (Antag.Stance)
                    {

                        case 1:
                            Console.WriteLine($"\nYou cast magic reflect then clench your jaw when you realize what's coming. \n" +
                                $"You take a nasty hit from the {Antag.Name}'s {weapon}.\n");
                            Protag.HP -= 40;
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 2:
                            Console.WriteLine($"\nYour magic reflect spell fizzles out about the same time the {Antag.Name} relaxes it's guard. \n");
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 3:
                            Console.WriteLine($"\nYou both hear he sound of your reflect spell dissipating \n" +
                                $" it breaks the insult duel the two of you had gotten into.\n");                           
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 4:

                            Console.WriteLine($"\nYour magic reflect works perfectly. \n" +
                                $"The {Antag.Name}'s {spell2} is sent speeding back with extra oompf! \n");
                            Antag.HP -= 30;
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }

                            Program.EntCont();
                            break;
                        case 5:
                            int which = rnd.Next(1, 100);
                            string boom;
                            if (which > 50) { boom = "BOOM! the two magic defenses suddenly collapse in towards you!\n"; } else { boom = $"BOOM! the two magic defenses suddenly explode outwards on the {Antag.Name}!\n"; }
                            Console.WriteLine($"\nThe {Antag.Name}'s magic shield and your magic reflect spell collide. \n" +
                                $"The two magics become unstable and start to build force constantly rebounding.\n" +
                                $"Faster an Louder until suddenly\n" + boom);
                            if (which > 50) { Protag.HP -= 30; } else { Antag.HP -= 30; }
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        case 6:
                            Console.WriteLine($"\nYou both use some sort of reflection magic.  \n" +
                                $"it feels like two magnets pushing against each other for a moment then nothing.");
                            Protag.Stamina += 10; if (Protag.Stamina > Protag.MaxStamina) { Protag.Stamina = Protag.MaxStamina; }
                            Program.EntCont();
                            break;
                        default:
                            Console.WriteLine($"\nThe battle devolves into a grasping clawing roll in the dirt. \n" +
                                $"it's clear you're evenly matched but it's also clear you both suck at this. \n");
                            Protag.HP -= 5; Antag.HP -= 5;
                            Program.EntCont();
                            break;


                    }
                            
                }

                
            } while (Antag.HP > 0 && Protag.HP > 0);



            Protag.UpdateStats();
            Protag.BattleChance -= 5;
            if (Protag.BattleChance < 20) { Protag.BattleChance = 20; }
            Protag.Fight = false;
            if (Antag.HP <= 0) { return true; }
            if (Protag.HP <= 0) { return false; }



            return true;
        }


        #region Unimplemented parts of ideas for possible updates
        /* Unimplemented ideas for future updates
         * 
         static void Telescape()
         {   // idea for possible expansion of battle system.
             // teleport around the area, giving a brief pause to combat, shuffling the order you face enemies.
             throw new NotImplementedException();

         }


         public static int Move()
         {
             // part of idea for possible future expasion of battle system.  
             // give each area a 2d array to position various trimmings/enemies, 
             // attacks could only be used on closer enemies, proximity to certain trimmings affect certain actions

             throw new NotImplementedException();
         }

         */
        #endregion
    }
}
