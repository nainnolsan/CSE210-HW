using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Mindfulness
{
    class ReflectingActivity : Activity
    {
        private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless.",
        };

        private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different?",
            "What could you learn from this experience?",
            "What did you learn about yourself?",
            "How can you keep this experience in mind?",
        };

        public ReflectingActivity()
            : base(
                "Reflecting Activity",
                "This activity helps you reflect on times you have shown strength and resilience."
            ) { }

        public override void RunActivity()
        {
            DisplayStartingMessage();
            Random rnd = new Random();
            string prompt = GetRandomPrompt(rnd);
            Console.WriteLine($"\nPrompt:\n> {prompt}\n");
            Console.Write("Take a few seconds to think");
            ShowSpinner(3);
            Console.WriteLine();

            List<string> questionsPool = _questions.OrderBy(x => rnd.Next()).ToList();
            DateTime end = DateTime.Now.AddSeconds(_duration);
            int index = 0;

            while (DateTime.Now < end)
            {
                if (index >= questionsPool.Count)
                {
                    questionsPool = _questions.OrderBy(x => rnd.Next()).ToList();
                    index = 0;
                }
                Console.WriteLine($"- {questionsPool[index++]} ");
                ShowSpinner(6);
            }

            DisplayEndingMessage();
        }

        private string GetRandomPrompt(Random rnd)
        {
            return _prompts[rnd.Next(_prompts.Count)];
        }
    }
}
