namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_LeftShoulder : FuzzySet
    {
        public double PeakPoint;
        public double LeftOffset;
        public double RightOffset;
        
        public FuzzySet_LeftShoulder(double mid, double left, double right) : base(mid  / 2)
        {
            PeakPoint = mid;
            LeftOffset = left;
            RightOffset = right;
        }
        
        public override double CalculateDOM(double d)
        {
            // Since the offset can actually be 0.
            if (RightOffset.Equals(0) && PeakPoint.Equals(d))
            {
                return 1.0;
            }

            // If d is inbetween right offset and peak
            if ((d >= PeakPoint) && (d < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;
                return grad * (d - PeakPoint) + 1.0;
            }
            
            // If it's greater than the peakpoint
            if (d < PeakPoint)
            {
                return 1.0;
            }
            
            // If it's not on this variable
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