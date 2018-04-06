namespace TheAnimalKingdom.FuzzyLogic.FuzzyTerms
{
    public class FuzzyTermSet : FuzzyTerm
    {
        private FuzzySet _fuzzSet;

        public FuzzyTermSet(FuzzyTermSet fts)
        {
            _fuzzSet = fts._fuzzSet;
        }
        
        public FuzzyTermSet(FuzzySet fuzz)
        {
            _fuzzSet = fuzz;
        }
        
        public override FuzzyTerm Clone()
        {
            return new FuzzyTermSet(this);
        }

        public override double GetDOM()
        {
            return _fuzzSet.GetDOM();
        }

        public override void ClearDOM()
        {
            _fuzzSet.ClearDOM();
        }

        public override void ORwithDOM(double value)
        {
            _fuzzSet.ORwithDOM(value);
        }
    }
}