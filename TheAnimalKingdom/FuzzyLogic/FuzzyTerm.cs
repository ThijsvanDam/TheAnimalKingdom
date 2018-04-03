namespace TheAnimalKingdom.FuzzyLogic
{
    public abstract class FuzzyTerm
    {
        public abstract void Clone();
        public abstract void GetDOM();
        public abstract void ClearDOM();
        public abstract void ORwithDOM(double d);
    }
}