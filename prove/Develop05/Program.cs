using System;

namespace Develop05
{
    class Program
    {
        static void Main(string[] args)
        {
            GoalManager manager = new GoalManager();
            LevelSystem levelSystem = new LevelSystem();
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine($"Score: {manager.GetScore()}");
                int currentLevel = levelSystem.CalculateLevel(manager.GetScore());
                Console.WriteLine($"Level: {currentLevel}");
                Console.WriteLine();
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Save Goals");
                Console.WriteLine("4. Load Goals");
                Console.WriteLine("5. Record Event");
                Console.WriteLine("6. Quit");
                Console.Write("Select a choice from the menu: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateGoal(manager);
                        break;
                    case "2":
                        manager.ListGoals();
                        break;
                    case "3":
                        SaveGoals(manager);
                        break;
                    case "4":
                        LoadGoals(manager);
                        break;
                    case "5":
                        RecordGoalEvent(manager, levelSystem);
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void CreateGoal(GoalManager manager)
        {
            Console.WriteLine("The types of Goals are:");
            Console.WriteLine("1. Simple Goal");
            Console.WriteLine("2. Eternal Goal");
            Console.WriteLine("3. Checklist Goal");
            Console.Write("Which type of goal would you like to create? ");
            string typeChoice = Console.ReadLine();

            Console.Write("What is the name of your goal? ");
            string name = Console.ReadLine();

            Console.Write("What is a short description of it? ");
            string description = Console.ReadLine();

            Console.Write("What is the amount of points associated with this goal? ");
            int points = int.Parse(Console.ReadLine());

            if (typeChoice == "1")
            {
                Goal goal = new SimpleGoal(name, description, points);
                manager.AddGoal(goal);
            }
            else if (typeChoice == "2")
            {
                Goal goal = new EternalGoal(name, description, points);
                manager.AddGoal(goal);
            }
            else if (typeChoice == "3")
            {
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int targetCount = int.Parse(Console.ReadLine());

                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonus = int.Parse(Console.ReadLine());

                Goal goal = new ChecklistGoal(name, description, points, targetCount, bonus);
                manager.AddGoal(goal);
            }
            else
            {
                Console.WriteLine("Invalid goal type.");
            }
        }

        static void SaveGoals(GoalManager manager)
        {
            Console.Write("What is the filename for the goal file? ");
            string filename = Console.ReadLine();
            manager.SaveToFile(filename);
            Console.WriteLine("Goals saved.");
        }

        static void LoadGoals(GoalManager manager)
        {
            Console.Write("What is the filename for the goal file? ");
            string filename = Console.ReadLine();
            manager.LoadFromFile(filename);
            Console.WriteLine("Goals loaded.");
        }

        static void RecordGoalEvent(GoalManager manager, LevelSystem levelSystem)
        {
            if (manager.GoalCount() == 0)
            {
                Console.WriteLine("No goals to record.");
                return;
            }

            Console.WriteLine("The goals are:");
            manager.ListGoalsWithIndex();
            Console.Write("Which goal did you accomplish? ");
            int index = int.Parse(Console.ReadLine());
            int goalIndex = index - 1;

            if (goalIndex < 0 || goalIndex >= manager.GoalCount())
            {
                Console.WriteLine("Invalid goal selection.");
                return;
            }

            int oldScore = manager.GetScore();
            int pointsEarned = manager.RecordEvent(goalIndex);
            manager.AddScore(pointsEarned);
            int newScore = manager.GetScore();

            Console.WriteLine($"You earned {pointsEarned} points.");
            Console.WriteLine($"You now have {newScore} points.");

            int oldLevel = levelSystem.CalculateLevel(oldScore);
            int newLevel = levelSystem.CalculateLevel(newScore);

            if (newLevel > oldLevel)
            {
                ShowLevelUpAnimation(newLevel);
            }
        }

        static void ShowLevelUpAnimation(int newLevel)
        {
            Console.WriteLine();
            Console.WriteLine("********************************");
            Console.WriteLine("********************************");
            Console.WriteLine("********* LEVEL  UP! ***********");
            Console.WriteLine("********************************");
            Console.WriteLine("********************************");
            Console.WriteLine($"You reached Level {newLevel}!");
        }
    }
}
