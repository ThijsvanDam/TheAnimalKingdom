using System.Collections.Generic;

namespace TheAnimalKingdom.FuzzyLogic.FuzzyTerms
{
    public class FuzzyTermOR : FuzzyTerm
    {
        public List<FuzzyTerm> FuzzyTerms = new List<FuzzyTerm>();

        public FuzzyTermOR()
        {
        }

        public FuzzyTermOR(FuzzyTermOR ftor)
        {
            foreach (FuzzyTerm term in ftor.FuzzyTerms)
            {
                FuzzyTerms.Add(term);
            }
        }

        public FuzzyTermOR(FuzzyTerm t1, FuzzyTerm t2)
        {
            FuzzyTerms.AddRange(new[]{t1, t2});
        }
        
        public FuzzyTermOR(FuzzyTerm t1, FuzzyTerm t2, FuzzyTerm t3)
        {
            FuzzyTerms.AddRange(new[]{t1, t2, t3});
        }

        public FuzzyTermOR(FuzzyTerm t1, FuzzyTerm t2, FuzzyTerm t3, FuzzyTerm t4)
        {
            FuzzyTerms.AddRange(new[]{t1, t2, t3, t4});
        }
        
        public override FuzzyTerm Clone()
        {
            return new FuzzyTermOR(this);
        }
        public override double GetDOM()
        {
            double maxDom = double.MinValue;
            foreach (FuzzyTerm term in FuzzyTerms)
            {
                if (term.GetDOM() > maxDom)
                {
                    maxDom = term.GetDOM();
                }
            }
            return maxDom;
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