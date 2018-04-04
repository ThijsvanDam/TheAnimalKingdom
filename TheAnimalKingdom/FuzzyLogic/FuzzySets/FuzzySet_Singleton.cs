namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_Singleton : FuzzySet
    {
        public double PeakPoint;
        
        public FuzzySet_Singleton(double mid) : base(mid)
        {
            PeakPoint = mid;
        }
        
        public override double CalculateDOM(double d)
        {
            return PeakPoint.Equals(d) ? 1.0 : 0.0;
        }

        public override double GetDOM()
        {
            throw new System.NotImplementedException();
        }

        public override void ClearDOM()
        {
            throw new System.NotImplementedException();
        }

        public override double ORwithDOM(double d)
        {
            throw new System.NotImplementedException();
        }
    }
}