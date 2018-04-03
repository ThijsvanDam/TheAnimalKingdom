using System.Collections.Generic;

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
        
        private List<FuzzySet> _fuzzySets;
        private Dictionary<string, FuzzySet> _m_MemberSets;

        private int m_dMinRange;
        private int m_dMaxRange;

        public FuzzyVariable(List<FuzzySet> fuzzySets, Dictionary<string, FuzzySet> mMemberSets, int mDMinRange, int mDMaxRange)
        {
            _fuzzySets = fuzzySets;
            _m_MemberSets = mMemberSets;
            m_dMinRange = mDMinRange;
            m_dMaxRange = mDMaxRange;
        }

        public void AddTriangularSet(string s, double a, double b, double c)
        {
            
        }

        public void AddRightShoulder(string s, double a, double b, double c)
        {
            
        }

        public void AddLeftShoulder(string s, double a, double b, double c)
        {
            
        }

        public void AddSingletonSet(string s, double a, double b, double c)
        {
            
        }
        
        public void Fuzzify(double a)
        {
            
        }

        public float DefuzzifyMaxAv()
        {
            return 0f;
        }

        public float DefuzzifyCentroid()
        {
            return 0f;
        }
    }
}