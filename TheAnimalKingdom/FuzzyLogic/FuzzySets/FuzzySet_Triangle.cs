namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_Triangle : FuzzySet
    {
        public double PeakPoint;
        public double LeftOffset;
        public double RightOffset;

        public FuzzySet_Triangle(double peakPoint, double leftOffset, double rightOffset) : base(peakPoint)
        {
            PeakPoint = peakPoint;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
        }
        
        public override double CalculateDOM(double d)
        {
            if ((RightOffset.Equals(0.0) && PeakPoint.Equals(d)) || (LeftOffset.Equals(0.0) && PeakPoint.Equals(d)))
            {
                return 1.0;
            }

            // If d is inside the left part of the triangle.
            if ((d <= PeakPoint) && (d >= (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;

                return grad * (d - (PeakPoint - LeftOffset));
            }
            
            // If d is inside the right part of the triangle.
            if ((d > PeakPoint) && (d < (PeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;

                return grad * (d - PeakPoint) + 1.0;
            }
            
            return 0.0;
        }
    }
}