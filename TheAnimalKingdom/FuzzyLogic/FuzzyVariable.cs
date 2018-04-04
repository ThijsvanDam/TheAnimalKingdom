using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using TheAnimalKingdom.FuzzyLogic.FuzzySets;
using TheAnimalKingdom.FuzzyLogic.FuzzyTerms;

namespace TheAnimalKingdom.FuzzyLogic
{
    public class FuzzyVariable
    {
        // Antecedents:
        // Hunger
        // Thirst
        // Distance
        
        // Consequents:
        // Desirability for GatherFood
        
//        private List<FuzzySet> _fuzzySets;
        public Dictionary<string, FuzzySet> MemberSets;

        public double MinRange;
        public double MaxRange;

        public FuzzyVariable()
        {
            MemberSets = new Dictionary<string, FuzzySet>();
            MinRange = 0;
            MaxRange = 0;
        }

        public void AdjustRangetoFit(double min, double max)
        {
            if (min < MinRange)
            {
                MinRange = min;
            }
            if (max > MaxRange)
            {
                MaxRange = max;
            }
        }

        public FuzzyTermSet AddLeftShoulderSet(string name, double minBound, double peakPoint, double maxBound)
        {
            AdjustRangetoFit(minBound, maxBound);
            FuzzySet f = new FuzzySet_LeftShoulder(peakPoint, peakPoint - minBound, maxBound - peakPoint);
            MemberSets.Add(name, f);
            return new FuzzyTermSet(f);
        }

        public FuzzyTermSet AddRightShoulderSet(string name, double minBound, double peakPoint, double maxBound)
        {
            AdjustRangetoFit(minBound, maxBound);
            FuzzySet f = new FuzzySet_RightShoulder(peakPoint, peakPoint - minBound, maxBound - peakPoint);
            MemberSets.Add(name, f);
            return new FuzzyTermSet(f);
        }
        
        public FuzzyTermSet AddTriangularSet(string name, double minBound, double peakPoint, double maxBound)
        {
            AdjustRangetoFit(minBound, maxBound);
            FuzzySet f = new FuzzySet_Triangle(peakPoint, peakPoint - minBound, maxBound - peakPoint);
            MemberSets.Add(name, f);
            return new FuzzyTermSet(f);
        }

        public FuzzyTermSet AddSingletonSet(string name, double minBound, double peakPoint, double maxBound)
        {
            AdjustRangetoFit(minBound, maxBound);
            FuzzySet f = new FuzzySet_Singleton(peakPoint);
            MemberSets.Add(name, f);
            return new FuzzyTermSet(f);
        }

        public FuzzyTermSet AddTrapezoidSet(string name, double minBound, double leftPeakPoint, double rightPeakPoint, double maxBound)
        {
            AdjustRangetoFit(minBound, maxBound);
            FuzzySet f = new FuzzySet_Trapezoid(leftPeakPoint - minBound, leftPeakPoint, rightPeakPoint, maxBound - rightPeakPoint);
            MemberSets.Add(name, f);
            return new FuzzyTermSet(f);
        }
        
        public void Fuzzify(double value)
        {
            foreach (KeyValuePair<string, FuzzySet> set in MemberSets)
            {
                set.Value.DOM = set.Value.CalculateDOM(value);
                Console.WriteLine(set.Key + " is " + set.Value.DOM);
            }
        }

        public double DefuzzifyMaxAv()
        {
            double bottom = 0.0;
            double top = 0.0;

            foreach (KeyValuePair<string, FuzzySet> set in MemberSets)
            {
                bottom += set.Value.GetDOM();
                top += set.Value.RepresentativeValue * set.Value.GetDOM();
            }

            if (bottom.Equals(0))
            {
                return 0.0;
            }

            return top / bottom;
        }

        public double DefuzzifyCentroid(int numSamples)
        {
            double step = (MaxRange - MinRange) / numSamples;

            double area = 0.0;
            double SOM = 0.0;

            for (int i = 1; i <= numSamples; i++)
            {
                foreach (KeyValuePair<string, FuzzySet> set in MemberSets)
                {
                    double contribution = Math.Min(set.Value.CalculateDOM(MinRange + i * step), set.Value.GetDOM());
                    area += contribution;
                    SOM += (MinRange + i * step) * contribution;
                }
            }

            if (area.Equals(0))
            {
                return 0.0;
            }
            return SOM / area;
        }

        public void Render(Graphics g, Point graphStart)
        {
            int graphBaseWidth = 150;
            int graphRange = (int) Math.Abs(MinRange - MaxRange);
            int graphHeight = 80;
            Pen graphColor = new Pen(Color.DeepPink, 3);
            
            g.DrawLine(graphColor, graphStart.X, graphStart.Y, graphStart.X, graphStart.Y + graphHeight);
            g.DrawLine(graphColor, graphStart.X, graphStart.Y + graphHeight, graphStart.X + graphBaseWidth, graphStart.Y + graphHeight);

            foreach (KeyValuePair<string, FuzzySet> f in MemberSets)
            {
                f.Value.Render(g, graphStart, graphBaseWidth, graphHeight, graphRange);
            }
        }
    }
}