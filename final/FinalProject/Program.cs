using System;

namespace BudgetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CategoryManager categoryManager = new CategoryManager();
            categoryManager.AddCategory(new EssentialCategory("Food"));
            categoryManager.AddCategory(new EssentialCategory("Rent"));
            categoryManager.AddCategory(new EssentialCategory("Transportation"));
            categoryManager.AddCategory(new EssentialCategory("Utilities"));
            DataStorage.LoadCategories(categoryManager);
            TransactionManager transactionManager = new TransactionManager();
            List<Transaction> loaded = DataStorage.LoadTransactions();
            foreach (var t in loaded) transactionManager.AddTransaction(t);
            Budget budget = DataStorage.LoadBudget();
            budget.SetTotalSpent(transactionManager.GetAllTransactions().Sum(x => x.Amount));
            User user = new User("User", budget, categoryManager, transactionManager);
            user.Login();
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("Menu: 1) Add Transaction  2) View Transactions  3) View Report  4) Set Monthly Budget  5) Manage Categories  6) Show Info  7) Save & Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        user.AddTransaction();
                        break;
                    case "2":
                        transactionManager.DisplayAllTransactions();
                        break;
                    case "3":
                        Console.Write("Enter year (YYYY): ");
                        int year = int.TryParse(Console.ReadLine(), out var y) ? y : DateTime.Now.Year;
                        Console.Write("Enter month (1-12): ");
                        int month = int.TryParse(Console.ReadLine(), out var m) ? m : DateTime.Now.Month;
                        FinancialReport fr = new FinancialReport(transactionManager, categoryManager);
                        fr.DisplayReport(year, month);
                        break;
                    case "4":
                        Console.Write("Monthly limit amount: ");
                        if (decimal.TryParse(Console.ReadLine(), out var mlimit))
                        {
                            user.SetMonthlyBudget(mlimit);
                            Console.WriteLine("Budget updated.");
                        }
                        else Console.WriteLine("Invalid amount.");
                        break;
                    case "5":
                        ManageCategoriesFlow(categoryManager);
                        break;
                    case "6":
                        user.GetUserInfo();
                        break;
                    case "7":
                        DataStorage.SaveTransactions(transactionManager.GetAllTransactions());
                        DataStorage.SaveBudget(budget);
                        DataStorage.SaveCategories(categoryManager);
                        Console.WriteLine("Saved. Exiting...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void ManageCategoriesFlow(CategoryManager cm)
        {
            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("Category Manager: 1) List  2) Add Category  3) Back");
                Console.Write("Option: ");
                string opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        var all = cm.GetAllCategories();
                        for (int i = 0; i < all.Count; i++)
                        {
                            Console.WriteLine($"{i+1}) {all[i].GetName()} ({(all[i] is EssentialCategory ? "Essential" : "NonEssential")})");
                        }
                        break;
                    case "2":
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Is essential? (y/n): ");
                        string yn = Console.ReadLine();
                        bool essential = yn?.Trim().ToLower() == "y";
                        if (essential) cm.AddCategory(new EssentialCategory(name));
                        else cm.AddCategory(new NonEssentialCategory(name));
                        Console.WriteLine("Category added.");
                        break;
                    case "3":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }
    }
}
