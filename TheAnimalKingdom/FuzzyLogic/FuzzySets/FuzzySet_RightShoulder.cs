using System.Windows.Forms;

namespace TheAnimalKingdom.FuzzyLogic.FuzzySets
{
    public class FuzzySet_RightShoulder : FuzzySet
    {
        public double PeakPoint;
        public double LeftOffset;
        public double RightOffset;
        
        public FuzzySet_RightShoulder(double peakPoint, double leftOffset, double rightOffset) : base(((peakPoint + rightOffset) + peakPoint) / 2)
        {
            PeakPoint = peakPoint;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
        }
        
        public override double CalculateDOM(double d)
        {
            // Since the offset can actually be 0.
            if (LeftOffset.Equals(0) && PeakPoint.Equals(d))
            {
                return 1.0;
            }

            // If d is inbetween left offset and peak
            if ((d <= PeakPoint) && (d > (PeakPoint - LeftOffset)))
            {
                double grad = 1.0 / LeftOffset;
                return grad * (d - (PeakPoint - LeftOffset));
            }
            
            // If it's greater than the peakpoint
            if (d > PeakPoint)
            {
                return 1.0;
            }
            
            // If it's not on this variable
            return 0.0;
        }
    }
}