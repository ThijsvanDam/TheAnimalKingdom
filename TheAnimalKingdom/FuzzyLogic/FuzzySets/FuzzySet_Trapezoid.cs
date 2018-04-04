namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_Trapezoid : FuzzySet
    {
        public double LeftPeakPoint;
        public double RightPeakPoint;
        public double LeftOffset;
        public double RightOffset;
        
        public FuzzySet_Trapezoid(double leftPeak, double rightPeak, double leftOffset, double rightOffset) : base((leftPeak + rightPeak) / 2)
        {
            LeftPeakPoint = leftPeak;
            RightPeakPoint = rightPeak;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
        }

        public override double CalculateDOM(double d)
        {
            
            if ((RightOffset.Equals(0.0) && RightPeakPoint.Equals(d)) || (RightOffset.Equals(0.0) && RightPeakPoint.Equals(d)))
            {
                return 1.0;
            }

            // If it's inside the parts of the trapezoid.
            if (d > LeftOffset && d < RightOffset)
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
            if ((d > RightPeakPoint) && (d < (RightPeakPoint + RightOffset)))
            {
                double grad = 1.0 / -RightOffset;

                return grad * (d - RightPeakPoint) + 1.0;
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