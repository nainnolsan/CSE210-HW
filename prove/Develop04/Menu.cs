using System;
using System.Threading;

namespace Mindfulness
{
    static class Menu
    {
        public static void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program\n");
                Console.WriteLine("1) Breathing Activity");
                Console.WriteLine("2) Reflecting Activity");
                Console.WriteLine("3) Listing Activity");
                Console.WriteLine("4) Quit");
                Console.Write("\nChoose an option: ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        new BreathingActivity().RunActivity();
                        Pause();
                        break;
                    case "2":
                        new ReflectingActivity().RunActivity();
                        Pause();
                        break;
                    case "3":
                        new ListingActivity().RunActivity();
                        Pause();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("\nInvalid option.");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        private static void Pause()
        {
            Console.WriteLine("\nPress ENTER to return to the menu...");
            Console.ReadLine();
        }
    }
}
