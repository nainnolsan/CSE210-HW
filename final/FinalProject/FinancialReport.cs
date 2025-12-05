using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetApp
{
    public class FinancialReport
    {
        private readonly TransactionManager tm;
        private readonly CategoryManager cm;

        public FinancialReport(TransactionManager tm, CategoryManager cm)
        {
            this.tm = tm;
            this.cm = cm;
        }

        public Dictionary<string, decimal> GetTotalByCategory(int year, int month)
        {
            var list = tm.GetTransactionsByMonth(year, month);
            return list.GroupBy(t => t.CategoryName).ToDictionary(g => g.Key, g => g.Sum(t => t.Amount));
        }

        public string GenerateMonthlySummary(int year, int month)
        {
            var totals = GetTotalByCategory(year, month);
            var totalSpent = totals.Values.Sum();
            var essentialTotal = totals.Where(kv => cm.GetCategoryByName(kv.Key) is EssentialCategory).Sum(kv => kv.Value);
            var nonEssentialTotal = totalSpent - essentialTotal;
            var lines = new List<string>();
            lines.Add($"Monthly Summary for {year}-{month:D2}");
            lines.Add($"Total spent: {totalSpent:C}");
            lines.Add($" - Essential: {essentialTotal:C}");
            lines.Add($" - Non-Essential: {nonEssentialTotal:C}");
            foreach (var kv in totals)
            {
                lines.Add($" - {kv.Key}: {kv.Value:C}");
            }
            return string.Join(Environment.NewLine, lines);
        }

        public void DisplayReport(int year, int month)
        {
            Console.WriteLine(GenerateMonthlySummary(year, month));
        }
    }
}
