using System;

namespace BudgetApp
{
    public class Budget
    {
        public decimal MonthlyLimit { get; private set; }
        public decimal TotalSpent { get; private set; }

        public void SetMonthlyLimit(decimal amount) => MonthlyLimit = amount;
        public void AddExpense(decimal amount) => TotalSpent += amount;
        public decimal GetRemainingBudget() => MonthlyLimit - TotalSpent;
        public bool IsOverBudget() => TotalSpent > MonthlyLimit;
        public void SetTotalSpent(decimal amount) => TotalSpent = amount;
        public void DisplayBudget()
        {
            Console.WriteLine($"Monthly Limit: {MonthlyLimit:C}");
            Console.WriteLine($"Total Spent: {TotalSpent:C}");
            Console.WriteLine($"Remaining: {GetRemainingBudget():C}");
            Console.WriteLine($"Over budget: {IsOverBudget()}");
        }
    }
}
