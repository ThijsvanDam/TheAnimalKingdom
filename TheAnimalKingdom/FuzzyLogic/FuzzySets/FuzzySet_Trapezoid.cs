namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_Trapezoid : FuzzySet
    {
        public double LeftOffset;
        public double LeftPeakPoint;
        public double RightPeakPoint;
        public double RightOffset;
        
        public FuzzySet_Trapezoid(double leftOffset, double leftPeak, double rightPeak, double rightOffset) : base((leftPeak + rightPeak) / 2)
        {
            LeftOffset = leftOffset;
            LeftPeakPoint = leftPeak;
            RightPeakPoint = rightPeak;
            RightOffset = rightOffset;
        }

        public override double CalculateDOM(double d)
        {
            
            if ((RightOffset.Equals(0.0) && RightPeakPoint.Equals(d)) || (RightOffset.Equals(0.0) && RightPeakPoint.Equals(d)))
            {
                return 1.0;
            }

            // If it's inside the parts of the trapezoid.
            if (d > LeftPeakPoint && d < RightPeakPoint)
            {
                return 1.0;
            }

            // If d is inside the left part of the triangle.
            if ((d <= LeftPeakPoint) && (d >= (LeftPeakPoint- LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;

                return grad * (d - (LeftPeakPoint - LeftOffset));
            }
            
            // If d is inside the right part of the triangle.
            if ((d >= RightPeakPoint) && (d < (RightPeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;

                return grad * (d - RightPeakPoint) + 1.0;
            }
            
            return 0.0;
        }
    }
}