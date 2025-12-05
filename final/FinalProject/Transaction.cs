using System;

namespace BudgetApp
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string CategoryName { get; set; }

        public Transaction(string description, decimal amount, DateTime date, string categoryName)
        {
            Id = Guid.NewGuid();
            Description = description;
            Amount = amount;
            Date = date;
            CategoryName = categoryName;
        }

        public Transaction() { }

        public decimal GetAmount() => Amount;
        public string GetCategory() => CategoryName;
        public void DisplayTransaction()
        {
            Console.WriteLine($"{Date:yyyy-MM-dd} | {Description} | {Amount:C} | {CategoryName}");
        }
    }
}
