namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_LeftShoulder : FuzzySet
    {
        public double PeakPoint;
        public double LeftOffset;
        public double RightOffset;
        
        public FuzzySet_LeftShoulder(double peakPoint, double leftOffset, double rightOffset) : base(peakPoint  / 2)
        {
            PeakPoint = peakPoint;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
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
    }
}