namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_Triangle : FuzzySet
    {
        public double PeakPoint;
        public double LeftOffset;
        public double RightOffset;

        public FuzzySet_Triangle(double mid, double left, double right) : base(mid)
        {
            PeakPoint = mid;
            LeftOffset = left;
            RightOffset = right;
        }
        
        public override double CalculateDOM(double d)
        {
            if ((RightOffset.Equals(0.0) && PeakPoint.Equals(d)) || (LeftOffset.Equals(0.0) && PeakPoint.Equals(d)))
            {
                return 1.0;
            }

            if ((d <= PeakPoint) && (d >= (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;

                return grad * (d - (PeakPoint - LeftOffset));
            }
            
            if ((d > PeakPoint) && (d < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / RightOffset;

                return grad * (d - PeakPoint) - 1.0;
            }
            
            return 0.0;
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