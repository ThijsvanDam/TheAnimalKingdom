using System.Collections.Generic;

namespace TheAnimalKingdom.FuzzyLogic.FuzzyTerms
{
    public class FuzzyTermAND : FuzzyTerm
    {
        // Could, according to the book, from two to four FuzzyTerms.
        
        public List<FuzzyTerm> FuzzyTerms = new List<FuzzyTerm>();

        public FuzzyTermAND(FuzzyTermAND ftand)
        {
            foreach (FuzzyTerm term in ftand.FuzzyTerms)
            {
                FuzzyTerms.Add(term);
            }
        }

        public FuzzyTermAND(FuzzyTerm t1, FuzzyTerm t2)
        {
            FuzzyTerms.AddRange(new[]{t1, t2});
        }
        
        public FuzzyTermAND(FuzzyTerm t1, FuzzyTerm t2, FuzzyTerm t3)
        {
            FuzzyTerms.AddRange(new[]{t1, t2, t3});
        }

        public FuzzyTermAND(FuzzyTerm t1, FuzzyTerm t2, FuzzyTerm t3, FuzzyTerm t4)
        {
            FuzzyTerms.AddRange(new[]{t1, t2, t3, t4});
        }
        
        public override FuzzyTerm Clone()
        {
            return new FuzzyTermAND(this);
        }

        public override double GetDOM()
        {
            double minDOM = double.MaxValue;
            foreach (FuzzyTerm term in FuzzyTerms)
            {
                if (term.GetDOM() < minDOM)
                {
                    minDOM = term.GetDOM();
                }
            }
            return minDOM;
        }

        public override void ClearDOM()
        {
            foreach (FuzzyTerm term in FuzzyTerms)
            {
                term.ClearDOM();
            }
        }

        public override void ORwithDOM(double value)
        {
            foreach (FuzzyTerm term in FuzzyTerms)
            {
                term.ORwithDOM(value);
            }
        }
    }
}