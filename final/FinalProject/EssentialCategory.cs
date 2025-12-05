namespace BudgetApp
{
    public class EssentialCategory : Category
    {
        public EssentialCategory(string name) : base(name) { }
        public override double CalculateImpactScore(decimal amount) => (double)amount * 0.8;
    }
}
