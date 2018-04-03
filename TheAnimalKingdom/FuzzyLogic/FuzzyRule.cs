namespace TheAnimalKingdom.FuzzyLogic
{
    public class FuzzyRule
    {
        private FuzzyTerm _m_pAntecedent;
        private FuzzyTerm _m_pConsequent;

        public FuzzyRule(FuzzyTerm mPAntecedent, FuzzyTerm mPConsequent)
        {
            _m_pAntecedent = mPAntecedent;
            _m_pConsequent = mPConsequent;
        }

        public void Calculate()
        {
            
        }
    }
}