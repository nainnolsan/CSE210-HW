using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetApp
{
    public class CategoryManager
    {
        private readonly Dictionary<string, Category> categories = new Dictionary<string, Category>(StringComparer.OrdinalIgnoreCase);

        public void AddCategory(Category c)
        {
            if (!categories.ContainsKey(c.GetName())) categories[c.GetName()] = c;
        }

        public Category GetCategoryByName(string name)
        {
            categories.TryGetValue(name, out var c);
            return c;
        }

        public List<Category> GetAllCategories() => categories.Values.ToList();
    }
}
