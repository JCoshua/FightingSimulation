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

        bool gameOver = false;
        Monster currentMonster1;
        Monster currentMonster2;
        int currentMonsterIndex = 1;
        Monster wompus;
        Monster thwompus;
        Monster mompus;
        Monster popus;


        void Start()
        {
            //Monster 1's Stats
            wompus.name = "Wompus";
            wompus.health = 30.0f;
            wompus.attack = 15.0f;
            wompus.defence = 5.0f;

            //Monster 2's Stats
            thwompus.name = "Thwompus";
            thwompus.health = 15.0f;
            thwompus.attack = 15.0f;
            thwompus.defence = 10.0f;

            //Monster 3's Stats
            mompus.name = "Mompus";
            mompus.health = 20.0f;
            mompus.attack = 10.0f;
            mompus.defence = 15.0f;

            //Monster 4's Stats
            popus.name = "Popus";
            popus.health = 25.0f;
            popus.attack = 25.0f;
            popus.defence = 0.0f;

            //Set starting fighters
            currentMonster1 = GetMonster(currentMonsterIndex);
            currentMonsterIndex++;
            currentMonster2 = GetMonster(currentMonsterIndex);
        }


        void Update()
        {
            Battle();
            UpdateCurrentMonsters();
            Console.ReadKey(true);
            Console.Clear();
        }

        Monster GetMonster(int monsterIndex)
        {
            Monster monster;
            monster.name = "None";
            monster.attack = 0.0f;
            monster.defence = 0.0f;
            monster.health = 0.0f;

            if (monsterIndex == 1)
            {
                monster = popus;
            }
            else if (monsterIndex == 2)
            {
                monster = mompus;
            }
            else if (monsterIndex == 3)
            {
                monster = wompus;
            }
            else if (monsterIndex == 4)
            {
                monster = thwompus;
            }

            return monster;
        }

        /// <summary>
        /// Simulates one turn in combat
        /// </summary>
        void Battle()
        {
            //Print Monster 1 Stats
            PrintStats(currentMonster1);
            //Print Monster 2 Stats
            PrintStats(currentMonster2);

            //Monster 2 attacks monster 1
            float damageTaken = Fight(currentMonster1, ref currentMonster2);
            Console.WriteLine(currentMonster2.name + " has taken " + damageTaken +  " damage.");

            damageTaken = Fight(currentMonster2, ref currentMonster1);
            Console.WriteLine(currentMonster1.name + " has taken " + damageTaken + " damage.");
        }

        /// <summary>
        /// Updates the monsters after a battle is over.
        /// Ends if all fighters in the list have been used.
        /// </summary>
        void UpdateCurrentMonsters()
        {
            //If Monster 1 dies...
            if (currentMonster1.health <= 0)
            {
                //...get the next monster
                currentMonsterIndex++;
                currentMonster1 = GetMonster(currentMonsterIndex);
            }

            //If Monster 2 dies...
            if (currentMonster2.health <= 0)
            {
                //...get the next monster
                currentMonsterIndex++;
                currentMonster2 = GetMonster(currentMonsterIndex);
            }

            //If a monster is set to "None" and the last monster has been set.
            if ((currentMonster1.name == "None" || currentMonster2.name == "None") && currentMonsterIndex > 5)
            {
                Console.WriteLine("Simulation Over");
                gameOver = true;
            }
        }

        void PrintStats (Monster monster)
        {
            Console.WriteLine("Name: " + monster.name);
            Console.WriteLine("Health: " + monster.health);
            Console.WriteLine("Attack: " + monster.attack);
            Console.WriteLine("Defense: " + monster.defence);
            Console.WriteLine();
        }

        float CalculateDamage(Monster attacker, Monster defender)
        {
            float damage = attacker.attack - defender.defence;
            if (damage <= 0)
            {
                damage = 1;
            }
            return damage;
        }

        float Fight(Monster attacker, ref Monster defender)
        {
            float damageTaken = CalculateDamage(attacker, defender);
            defender.health -= damageTaken;
            return damageTaken;
        }

        string StartBattle(ref Monster monster1, ref Monster monster2)
        {
            string battleResult = "No Contest";
            while (monster1.health > 0 && monster2.health > 0)
            {
                //Print Monster 1 Stats
                PrintStats(monster1);
                //Print Monster 2 Stats
                PrintStats(monster2);

                Fight(monster1, ref monster2);
                Fight(monster2, ref monster1);
                Console.ReadKey(true);
                Console.Clear();
            }

            if (monster1.health <= 0 && monster2.health <= 0)
            {
                battleResult ="Draw";
            }
            else if (monster1.health > 0)
            {
                battleResult = monster1.name;
            }
            else if (monster2.health > 0)
            {
                battleResult = monster2.name;
            }

            return battleResult;
        }

        public void Run()
        {
            Start();

            while (!gameOver)
            {
                Update();
            }
        }
    }
}
