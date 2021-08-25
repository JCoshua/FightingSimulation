using System;
using System.Collections.Generic;
using System.Text;

namespace FightingSimulation
{
    public struct Monster
    {
       public string name;
       public float health;
       public float attack;
       public float defence;
    }

    class Game
    {
        void PrintStats (Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defence);
            Console.WriteLine();
        }

        float CalculateDamage(float attacker, float defender)
        {
            float damage = attacker - defender;
            if (damage <= 0)
            {
                damage = 1; 
            }
            return damage;
        }

        public void Run()
        {   //Monster 1's Stats
            Monster monster1;
            monster1.name = "Wompus";
            monster1.health = 20.0f;
            monster1.attack = 10.0f;
            monster1.defence = 5.0f;

            //Monster 2's Stats
            Monster monster2;
            monster2.name = "Thwompus";
            monster2.health = 15.0f;
            monster2.attack = 15.0f;
            monster2.defence = 10.0f;

            //Print Monster 1 Stats
            PrintStats(monster1);
            //Print Monster 2 Stats
            PrintStats(monster2);

            //Monster 1 attacks monster 2
            float damageTaken = CalculateDamage(monster1.attack, monster2.defence);
            monster2.health -= damageTaken;
            Console.WriteLine(monster2.name + " has taken " + damageTaken + " damage");

            //Monster 2 attacks monster 1
            damageTaken = CalculateDamage(monster2.attack, monster1.defence);
            monster1.health -= damageTaken;
            Console.WriteLine(monster1.name + " has taken " + damageTaken + " damage");

            Console.ReadKey();
            Console.Clear();

            //Print Monster 1 Stats
            PrintStats(monster1);
            //Print Monster 2 Stats
            PrintStats(monster2);
            Console.ReadKey();
        }
    }
}
