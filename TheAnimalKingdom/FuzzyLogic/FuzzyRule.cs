namespace TheAnimalKingdom.FuzzyLogic
{
    public class FuzzyRule
    {
        // Usually a composite of several fuzzy sets and operators
        private FuzzyTerm Antecedent;
        
        // Usually a single fuzzy set
        private FuzzyTerm Consequence;

        public FuzzyRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            Antecedent = antecedent.Clone();
            Consequence = consequence.Clone();
        }

        public void SetConfidenceOfConsequentToZero()
        {
            Consequence.ClearDOM();
        }
        
        // Updates the DOM of the consequent term with the DOM of the antecedent term
        public void Calculate()
        {
            Consequence.ORwithDOM(Antecedent.GetDOM());
        }
    }
}