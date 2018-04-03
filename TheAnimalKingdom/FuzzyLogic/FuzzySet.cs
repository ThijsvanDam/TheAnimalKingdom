namespace TheAnimalKingdom.FuzzyLogic
{
    public abstract class FuzzySet
    {
        // Antecedents:
        // Full, CouldEat, Hungry
        // Saturated, CouldDrink, Thirsty
        // Close, Middle, Far
        
        // Consequents:
        // Undesirable, Desirable, VeryDesirable

        public FuzzySet(double representative)
        {
            RepresentativeValue = representative;
        }
        
        public double m_dDOM;
        public double RepresentativeValue;

        
        public abstract double CalculateDOM(double d);
        public abstract double GetDOM();
        public abstract void ClearDOM();
        public abstract double ORwithDOM(double d);
    }
}