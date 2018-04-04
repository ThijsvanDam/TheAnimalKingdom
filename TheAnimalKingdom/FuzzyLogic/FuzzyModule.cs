using System.Collections.Generic;
using System.Data;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.FuzzyLogic
{
    public class FuzzyModule
    {
        
        private Dictionary<string, FuzzyVariable> Variables;
        private List<FuzzyRule> Rules;
        
        public FuzzyModule()
        {
            Variables = new Dictionary<string, FuzzyVariable>();
            Rules = new List<FuzzyRule>();
        }
        
//        private void RunRules()
//        {
//            
//        }

        public FuzzyVariable GetVariable(string s)
        {
            return Variables[s];
        }
        
        public FuzzyVariable CreateFLV(string name)
        {
            var f = new FuzzyVariable();
            Variables.Add(name, f);
            return f;
        }
        
        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            Rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public void SetConfidencesOfConsequentsToZero()
        {
            foreach (FuzzyRule r in Rules)
            {
                r.SetConfidenceOfConsequentToZero();
            }
        }

        public void Fuzzify(string name, double val)
        {
            Variables[name].Fuzzify(val);
        }

        public double Defuzzify(string name, DefuzzifyMethod method)
        {
            // Make sure the FLV exists in this module
            if (!Variables.ContainsKey(name))
            {
                throw new KeyNotFoundException();
            }

            // Clear all consequent DOMs
            SetConfidencesOfConsequentsToZero();

            // Process the rules
            foreach (FuzzyRule rule in Rules)
            {
                rule.Calculate();
            }
            
            // Defuzzify the resultant conclusion using the specified method
            switch (method)
            {
                    case DefuzzifyMethod.Centroid:
                        int NumSamples = 15;
                        return Variables[name].DefuzzifyCentroid(NumSamples);
                    case DefuzzifyMethod.MaxAV:
                        return Variables[name].DefuzzifyMaxAv();
            }
            return 0;
        }

    }
}