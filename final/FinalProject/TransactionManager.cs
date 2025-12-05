using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetApp
{
    public class TransactionManager
    {
        private readonly List<Transaction> transactions = new List<Transaction>();

        public List<Transaction> GetAllTransactions() => transactions;

        public void AddTransaction(Transaction t) => transactions.Add(t);

        public void RemoveTransaction(Guid id) => transactions.RemoveAll(x => x.Id == id);

        public List<Transaction> GetTransactionsByCategory(string name) =>
            transactions.Where(t => string.Equals(t.CategoryName, name, StringComparison.OrdinalIgnoreCase)).ToList();

        public List<Transaction> GetTransactionsByMonth(int year, int month) =>
            transactions.Where(t => t.Date.Year == year && t.Date.Month == month).ToList();

        public void DisplayAllTransactions()
        {
            if (!transactions.Any())
            {
                Console.WriteLine("No transactions.");
                return;
            }
            foreach (var t in transactions.OrderByDescending(x => x.Date))
            {
                t.DisplayTransaction();
            }
        }
    }
}
