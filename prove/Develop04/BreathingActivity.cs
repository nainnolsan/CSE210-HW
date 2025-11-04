using System;
using System.Threading;

namespace Mindfulness
{
    class BreathingActivity : Activity
    {
        public BreathingActivity()
            : base(
                "Breathing Activity",
                "This activity helps you relax by guiding you through slow breathing. Focus on your breath."
            ) { }

        public override void RunActivity()
        {
            DisplayStartingMessage();
            DateTime end = DateTime.Now.AddSeconds(_duration);
            while (DateTime.Now < end)
            {
                Console.Write("Breathe in... ");
                ShowCountdown(4);
                Console.WriteLine();
                Console.Write("Breathe out... ");
                ShowCountdown(6);
                Console.WriteLine();
            }
            DisplayEndingMessage();
        }
    }
}
