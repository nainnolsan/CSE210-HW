using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BudgetApp
{
    public static class DataStorage
    {
        public static string TransactionsFile = "transactions.txt";
        public static string BudgetFile = "budget.txt";
        public static string CategoriesFile = "categories.txt";

        public static void SaveTransactions(List<Transaction> transactions)
        {
            var lines = transactions.Select(t => $"{t.Id}|{Escape(t.Description)}|{t.Amount}|{t.Date:O}|{Escape(t.CategoryName)}").ToArray();
            File.WriteAllLines(TransactionsFile, lines);
        }

        public static List<Transaction> LoadTransactions()
        {
            var list = new List<Transaction>();
            if (!File.Exists(TransactionsFile)) return list;
            var lines = File.ReadAllLines(TransactionsFile);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts.Length < 5) continue;
                if (!Guid.TryParse(parts[0], out var id)) id = Guid.NewGuid();
                string desc = Unescape(parts[1]);
                if (!decimal.TryParse(parts[2], out var amount)) amount = 0m;
                DateTime date = DateTime.TryParse(parts[3], out var d) ? d : DateTime.Now;
                string cat = Unescape(parts[4]);
                var t = new Transaction(desc, amount, date, cat) { Id = id };
                list.Add(t);
            }
            return list;
        }

        public static void SaveBudget(Budget budget)
        {
            var line = $"{budget.MonthlyLimit}|{budget.TotalSpent}";
            File.WriteAllText(BudgetFile, line);
        }

        public static Budget LoadBudget()
        {
            var b = new Budget();
            if (!File.Exists(BudgetFile)) return b;
            var line = File.ReadAllText(BudgetFile);
            var parts = line.Split('|');
            if (parts.Length >= 1 && decimal.TryParse(parts[0], out var ml)) b.SetMonthlyLimit(ml);
            if (parts.Length >= 2 && decimal.TryParse(parts[1], out var ts)) b.SetTotalSpent(ts);
            return b;
        }

        public static void SaveCategories(CategoryManager cm)
        {
            var lines = cm.GetAllCategories().Select(c => $"{Escape(c.GetName())}|{(c is EssentialCategory ? "true" : "false")}").ToArray();
            File.WriteAllLines(CategoriesFile, lines);
        }

        public static void LoadCategories(CategoryManager cm)
        {
            if (!File.Exists(CategoriesFile)) return;
            var lines = File.ReadAllLines(CategoriesFile);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var parts = line.Split('|');
                if (parts.Length < 2) continue;
                string name = Unescape(parts[0]);
                bool essential = parts[1].Trim().ToLower() == "true";
                if (essential) cm.AddCategory(new EssentialCategory(name));
                else cm.AddCategory(new NonEssentialCategory(name));
            }
        }

        static string Escape(string s)
        {
            return s?.Replace("|", "/|/") ?? "";
        }

        static string Unescape(string s)
        {
            return s?.Replace("/|/", "|") ?? "";
        }
    }
}
