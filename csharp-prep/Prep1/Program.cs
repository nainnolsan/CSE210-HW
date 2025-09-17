using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Whats is your first name?");
        string first_name = Console.ReadLine();

        Console.WriteLine("Whats is your last name?");
        string last_name = Console.ReadLine();

        Console.WriteLine($"Your name is {last_name}, {first_name} {last_name}.");
    }
}