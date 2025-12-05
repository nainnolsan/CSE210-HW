namespace BudgetApp
{
    public abstract class Category
    {
        public string Name { get; private set; }
        protected Category(string name) => Name = name;
        public string GetName() => Name;
        public virtual double CalculateImpactScore(decimal amount) => (double)amount;
    }
}
