namespace BudgetApp
{
    public class NonEssentialCategory : Category
    {
        public NonEssentialCategory(string name) : base(name) { }
        public override double CalculateImpactScore(decimal amount) => (double)amount * 1.2;
    }
}
