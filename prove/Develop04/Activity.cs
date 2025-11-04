using System;
using System.IO;
using System.Threading;

namespace Mindfulness
{
    abstract class Activity
    {
        protected string _name;
        protected string _description;
        protected int _duration;

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        protected void DisplayStartingMessage()
        {
            Console.Clear();
            Console.WriteLine($"--- {_name} ---\n");
            Console.WriteLine(_description + "\n");
            Console.Write("Enter duration in seconds: ");
            _duration = int.Parse(Console.ReadLine() ?? "30");
            Console.WriteLine("\nPrepare to begin...");
            ShowSpinner(3);
        }

        protected void DisplayEndingMessage()
        {
            Console.WriteLine("\nWell done!\n");
            ShowSpinner(2);
            Console.WriteLine($"You completed {_name} for {_duration} seconds.");
            ShowSpinner(3);
            LogSession();
        }

        protected void ShowSpinner(int seconds)
        {
            char[] frames = { '|', '/', '-', '\\' };
            DateTime end = DateTime.Now.AddSeconds(seconds);
            int i = 0;
            while (DateTime.Now < end)
            {
                Console.Write(frames[i++ % frames.Length]);
                Thread.Sleep(120);
                Console.Write("\b \b");
            }
        }

        protected void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write($"{i}");
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        public abstract void RunActivity();

        private void LogSession()
        {
            File.AppendAllLines(
                "session_log.txt",
                new[] { $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}\t{_name}\t{_duration}" }
            );
        }
    }
}
