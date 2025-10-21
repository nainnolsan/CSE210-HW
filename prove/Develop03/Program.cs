using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart and lean not unto thine own understanding.";
        Scripture scripture = new Scripture(reference, text);

        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());

        while (true)
        {
            Console.WriteLine("\nPress Enter to hide words or type 'quit' to end.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3);
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nAll words are hidden! Good job memorizing!");
                break;
            }
        }

        // Creativity & Extra Credit:
        // I added a feature that gives a random motivational message
        // after all words are hidden to encourage memorization progress.
        string[] messages = {
            "You did it! Keep the word in your heart!",
            "Great job! Another verse memorized!",
            "Awesome! You’re becoming a scripture master!"
        };

        if (scripture.IsCompletelyHidden())
        {
            Random rnd = new Random();
            Console.WriteLine(messages[rnd.Next(messages.Length)]);
        }
    }
}