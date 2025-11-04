using System;
using System.Collections.Generic;
using System.Threading;

namespace Mindfulness
{
    class ListingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?",
        };

        public ListingActivity()
            : base(
                "Listing Activity",
                "This activity helps you reflect on good things in your life by listing positive items."
            ) { }

        public override void RunActivity()
        {
            DisplayStartingMessage();
            Random rnd = new Random();
            string prompt = GetRandomPrompt(rnd);
            Console.WriteLine($"Prompt:\n> {prompt}\n");
            Console.WriteLine("Starting in:");
            ShowCountdown(5);
            Console.WriteLine("\nStart listing! (Press ENTER after each item)\n");

            List<string> responses = GetUserResponses();
            Console.WriteLine($"\nYou listed {responses.Count} items. Great job!");
            DisplayEndingMessage();
        }

        private string GetRandomPrompt(Random rnd)
        {
            return _prompts[rnd.Next(_prompts.Count)];
        }

        private List<string> GetUserResponses()
        {
            List<string> responses = new List<string>();
            DateTime end = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < end)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    responses.Add(input.Trim());
                }
                else
                {
                    break;
                }
            }
            return responses;
        }
    }
}
