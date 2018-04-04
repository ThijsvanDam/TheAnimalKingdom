namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_Singleton : FuzzySet
    {
        public double PeakPoint;
        
        public FuzzySet_Singleton(double peakPoint) : base(peakPoint)
        {
            PeakPoint = peakPoint;
        }
        
        public override double CalculateDOM(double d)
        {
            return PeakPoint.Equals(d) ? 1.0 : 0.0;
        }
    }
}