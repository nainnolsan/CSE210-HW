using System;

namespace BudgetApp
{
    public class User
    {
        public string Name { get; set; }
        public Budget Budget { get; set; }
        public CategoryManager Categories { get; set; }
        public TransactionManager Transactions { get; set; }

        public User(string name, Budget budget, CategoryManager categories, TransactionManager transactions)
        {
            Name = name;
            Budget = budget;
            Categories = categories;
            Transactions = transactions;
        }

        public void Login()
        {
            Console.WriteLine($"Welcome, {Name}!");
        }

        public void AddTransaction()
        {
            Console.Write("Description: ");
            string desc = Console.ReadLine() ?? "";
            Console.Write("Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out var amount))
            {
                Console.WriteLine("Invalid amount.");
                return;
            }
            Console.Write("Date (YYYY-MM-DD) leave empty for today: ");
            string dateStr = Console.ReadLine();
            DateTime date = string.IsNullOrWhiteSpace(dateStr) ? DateTime.Now : DateTime.TryParse(dateStr, out var d) ? d : DateTime.Now;
            var cats = Categories.GetAllCategories();
            Console.WriteLine("Available categories:");
            for (int i = 0; i < cats.Count; i++) Console.WriteLine($"{i+1}) {cats[i].GetName()} ({(cats[i] is EssentialCategory ? "Essential" : "NonEssential")})");
            Console.Write("Choose category number or type a new category name: ");
            var catInput = Console.ReadLine();
            string chosenCategoryName;
            if (int.TryParse(catInput, out var idx) && idx >= 1 && idx <= cats.Count)
            {
                chosenCategoryName = cats[idx - 1].GetName();
            }
            else
            {
                chosenCategoryName = catInput ?? "Uncategorized";
                Console.Write("Is this category essential? (y/n): ");
                string yn = Console.ReadLine();
                bool essential = yn?.Trim().ToLower() == "y";
                if (essential) Categories.AddCategory(new EssentialCategory(chosenCategoryName));
                else Categories.AddCategory(new NonEssentialCategory(chosenCategoryName));
            }
            var t = new Transaction(desc, amount, date, chosenCategoryName);
            Transactions.AddTransaction(t);
            Budget.AddExpense(amount);
            Console.WriteLine("Transaction added:");
            t.DisplayTransaction();
        }

        public void ViewReport(int year, int month)
        {
            var fr = new FinancialReport(Transactions, Categories);
            fr.DisplayReport(year, month);
        }

        public void SetMonthlyBudget(decimal amount) => Budget.SetMonthlyLimit(amount);

        public void GetUserInfo()
        {
            Console.WriteLine($"User: {Name}");
            Budget.DisplayBudget();
            Console.WriteLine($"Categories count: {Categories.GetAllCategories().Count}");
            Console.WriteLine($"Transactions count: {Transactions.GetAllTransactions().Count}");
        }
    }
}
