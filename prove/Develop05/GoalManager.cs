using System;
using System.Collections.Generic;
using System.IO;

namespace Develop05
{
    class GoalManager
    {
        private List<Goal> _goals;
        private int _score;

        public GoalManager()
        {
            _goals = new List<Goal>();
            _score = 0;
        }

        public void AddGoal(Goal goal)
        {
            _goals.Add(goal);
        }

        public int GetScore()
        {
            return _score;
        }

        public void AddScore(int points)
        {
            _score += points;
        }

        public int GoalCount()
        {
            return _goals.Count;
        }

        public void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals have been created.");
                return;
            }

            int index = 1;
            foreach (Goal goal in _goals)
            {
                Console.WriteLine($"{index}. {goal.GetDetailsString()}");
                index++;
            }
        }

        public void ListGoalsWithIndex()
        {
            int index = 1;
            foreach (Goal goal in _goals)
            {
                Console.WriteLine($"{index}. {goal.GetDetailsString()}");
                index++;
            }
        }

        public int RecordEvent(int index)
        {
            Goal goal = _goals[index];
            int points = goal.RecordEvent();
            return points;
        }

        public void SaveToFile(string filename)
        {
            using (StreamWriter outputFile = new StreamWriter(filename))
            {
                outputFile.WriteLine(_score);
                foreach (Goal goal in _goals)
                {
                    string line = goal.GetStringRepresentation();
                    outputFile.WriteLine(line);
                }
            }
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                return;
            }

            string[] lines = File.ReadAllLines(filename);
            _goals.Clear();

            if (lines.Length == 0)
            {
                _score = 0;
                return;
            }

            _score = int.Parse(lines[0]);

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] parts = line.Split(':');
                if (parts.Length != 2)
                {
                    continue;
                }

                string type = parts[0];
                string data = parts[1];
                string[] values = data.Split('|');

                if (type == "SimpleGoal" && values.Length >= 4)
                {
                    string name = values[0];
                    string description = values[1];
                    int points = int.Parse(values[2]);
                    bool isComplete = bool.Parse(values[3]);
                    SimpleGoal goal = new SimpleGoal(name, description, points);
                    if (isComplete)
                    {
                        goal.MarkComplete();
                    }
                    _goals.Add(goal);
                }
                else if (type == "EternalGoal" && values.Length >= 3)
                {
                    string name = values[0];
                    string description = values[1];
                    int points = int.Parse(values[2]);
                    EternalGoal goal = new EternalGoal(name, description, points);
                    _goals.Add(goal);
                }
                else if (type == "ChecklistGoal" && values.Length >= 6)
                {
                    string name = values[0];
                    string description = values[1];
                    int points = int.Parse(values[2]);
                    int targetCount = int.Parse(values[3]);
                    int bonus = int.Parse(values[4]);
                    int currentCount = int.Parse(values[5]);
                    ChecklistGoal goal = new ChecklistGoal(name, description, points, targetCount, bonus);
                    goal.SetCurrentCount(currentCount);
                    _goals.Add(goal);
                }
            }
        }
    }
}
