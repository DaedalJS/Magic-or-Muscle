using System;
using System.Collections.Generic;
using System.Text;

namespace TextBasedGame
{
    class Enemy
    {
        public Enemy(int number, string name, int hP, int mP, int type)
        {
            Number = number;
            Name = name;
            HP = hP;
            MP = mP;
            Type = type;
            Stance = 1;
        }

        public int Number { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int MP { get; set; }
        public int Type { get; set; }
        public int Stance { get; set; }

        public void EnemyFightChoice(Random rnd)
        {
            if (Type == 1)
            {
                Stance = rnd.Next(4, 6);

            }
            if (Type == 2)
            {
                Stance = rnd.Next(1, 3);
             
            }
            if (Type == 3)
            {
                Stance = rnd.Next(1, 6);
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine($"{Name} | Remaining HP: {HP} \n");
        }


    }
}
