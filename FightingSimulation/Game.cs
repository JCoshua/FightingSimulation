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

        //Monsters
        Monster wompus;
        Monster thwompus;
        Monster mompus;
        Monster knuckles;
        Monster[] monsters;


        int currentScene = 0;
        int currentMonsterIndex = 0;


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
            mompus.health = 30.0f;
            mompus.attack = 10.0f;
            mompus.defence = 15.0f;

            //Monster 4's Stats
            knuckles.name = "Knuckles";
            knuckles.health = 25.0f;
            knuckles.attack = 25.0f;
            knuckles.defence = 0.0f;

            monsters = new Monster[] { wompus, thwompus, mompus, knuckles };
            ResetCurrentMonster();
        }


        /// <summary>
        /// Condense code to a single function
        /// </summary>
        void Update()
        {
            UpdateCurrentScene();
            Console.Clear();
        }

        public void Run()
        {
            Start();

            while (!gameOver)
            {
                Update();
            }

            End();
        }

       
    void End()
        {
            Console.WriteLine("Goodbye");
        }

        /// <summary>
        /// Gets an input from the player
        /// </summary>
        /// <param name="description">The quesition or situation that prompts the decision</param>
        /// <param name="option1">The first choice</param>
        /// <param name="option2">The second choice</param>
        /// <param name="pauseInvalid">If true, the playe must press a key after inputing an invalid value.</param>
        /// <returns>A number representing the option choice. Returns 0 as default.</returns>
        int GetInput(string description, string option1, string option2, bool pauseInvalid = true)
        {
            //Prints the decription and options
            Console.WriteLine(description);
            Console.WriteLine("1. " + option1);
            Console.WriteLine("2. " + option2);

            //gets the input
            string input = Console.ReadLine();
            int choice = 0;

            //The Player choices option 1
            if (input == "1" || input.ToLower() == option1)
            {
                choice = 1;
            }

            //The Player choices option 2
            else if (input == "2" || input.ToLower() == option2)
            {
                choice = 2;
            }

            //Player does not enter a valid input
            else
            {
                Console.WriteLine("Invalid Input");

                //Requires input after getting error message.
                if (pauseInvalid)
                {
                    Console.ReadKey(true);
                }
            }

            Console.Clear();
            return choice;
        }

        /// <summary>
        /// Displays the start menu. They can start or leave
        /// </summary>
        void DisplayMainMenu()
        {
            int choice = GetInput("Welcome to Monster Fighter Simulators & Knuckles", "Start Simulation", "Embrace Cowardice");


            //If the chose to start the simulation
            if (choice == 1)
            {
                //start battle
                currentScene = 1;
            }

            //If the chose to embrace cowardice
            else if (choice == 2)
            {
                //let them cower in fear
                gameOver = true;
            }
        }

        /// <summary>
        /// Displays the restart menu.
        /// </summary>
        void DisplayRestartMenu()
        {
            //Get the player choice
            int choice = GetInput("Simulation over. Play Again?", "Yes", "No");

            //If the player cose to restart
            if (choice == 1)
            {
                ResetCurrentMonster();
                currentScene = 0;
            }

            //If the player quits
            else if (choice == 2)
            {
                gameOver = true;
            }
        }


        /// <summary>
        /// Resets the currentMonsterIndex tp the first two mpnsters in the array.
        /// </summary>
        void ResetCurrentMonster()
        {
            currentMonsterIndex = 0;
            currentMonster1 = monsters[currentMonsterIndex];
            currentMonsterIndex++;
            currentMonster2 = monsters[currentMonsterIndex];
        }

        void UpdateCurrentScene()
        {

            switch (currentScene)
            {
                case 0:
                    DisplayMainMenu();
                    break;

                case 1:
                    Battle();
                    UpdateCurrentMonsters();
                    Console.ReadKey(true);
                    Console.Clear();
                    break;

                case 2:
                    DisplayRestartMenu();
                    break;

                default:
                    Console.WriteLine("Invalid Scene Index");
                    break;
            }
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
            Console.WriteLine(currentMonster2.name + " has taken " + damageTaken + " damage.");

            damageTaken = Fight(currentMonster2, ref currentMonster1);
            Console.WriteLine(currentMonster1.name + " has taken " + damageTaken + " damage.");
        }

        bool TryEndSimulation()
        {
            bool simiulationOver = currentMonsterIndex >= monsters.Length;
            if (simiulationOver)
            {
                Console.Clear();
                PrintStats(currentMonster1);
                PrintStats(currentMonster2);
                Console.WriteLine("Simulation Complete");
                currentScene = 2;
            }
            return simiulationOver;
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
                if(TryEndSimulation())
                {
                    return;
                }
                currentMonster1 = monsters[currentMonsterIndex];
            }

            //If Monster 2 dies...
            if (currentMonster2.health <= 0)
            {
                //...get the next monster
                currentMonsterIndex++;
                if (TryEndSimulation())
                {
                    return;
                }
                    currentMonster2 = monsters[currentMonsterIndex];
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
    }
}
