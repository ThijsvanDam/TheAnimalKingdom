using System;
using System.Drawing;

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
        
        public double DOM;
        public double RepresentativeValue;

        public FuzzySet(double representative)
        {
            DOM = 0.0;
            RepresentativeValue = representative;
        }

        public abstract double CalculateDOM(double d);

        public double GetDOM()
        {
            return DOM;
        }

        public void ClearDOM()
        {
            DOM = 0.0;
        }

        public void ORwithDOM(double val)
        {
            if (val > DOM)
            {
                DOM = val;
            }
        }

        public void Render(Graphics g, Point graphStart, int graphBaseWidth, int graphHeight, int graphRange)
        {
            double drawSteps = 100;

            double baseSteps = graphBaseWidth / drawSteps;
            double rangeSteps = graphRange / drawSteps;
            
            Pen lineColor = new Pen(Color.DeepSkyBlue, 2);
            
            Point[] points = new Point[(int)drawSteps];

            Point zeroZero = new Point(graphStart.X, graphStart.Y + graphHeight);
            
            for (int i = 0; i < (graphBaseWidth / baseSteps); i++)
            {
                points[i] = new Point((int)(zeroZero.X + (i * baseSteps)), (int)(zeroZero.Y - (CalculateDOM(i * rangeSteps) * 50)));
            }
            
            g.DrawLines(lineColor, points);
        }
    }
}