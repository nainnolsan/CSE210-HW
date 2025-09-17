using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcome();

        string userName = PromptUserName();

        int favoriteNumber = PromptUserNumber();

        int birthYear;
        PromptUserBirthYear(out birthYear);

        int squaredNumber = SquareNumber(favoriteNumber);

        DisplayResult(userName, squaredNumber, birthYear);
    }

    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.Write("Please enter your name ");
        return Console.ReadLine();
    }

    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number ");
        return int.Parse(Console.ReadLine());
    }

    static void PromptUserBirthYear(out int birthYear)
    {
        Console.Write("Please enter the year you were born ");
        birthYear = int.Parse(Console.ReadLine());
    }

    static int SquareNumber(int number)
    {
        return number * number;
    }

    static void DisplayResult(string userName, int squaredNumber, int birthYear)
    {
        Console.WriteLine($"{userName}, the square of your number is {squaredNumber}");

        int currentYear = DateTime.Now.Year;
        int age = currentYear - birthYear;

        Console.WriteLine($"{userName}, you will turn {age} this year.");
    }
}