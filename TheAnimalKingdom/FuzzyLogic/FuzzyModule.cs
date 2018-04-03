using System.Collections.Generic;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.FuzzyLogic
{
    public class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> _m_Variables;
        private List<FuzzyRule> _m_Rules;

        private void RunRules()
        {
            
        }

        public FuzzyVariable CreateFLV(string name)
        {
            var f = new FuzzyVariable(null, null, 0, 0);
            _m_Variables.Add(name, f);
            return f;
        }
        
        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            _m_Rules.Add(new FuzzyRule(antecedent, consequence));
            
        }

        public void Fuzzify(string name, double val)
        {
            
        }

        public double Defuzzify(string name)
        {
            return 0.0;
        }

    }
}