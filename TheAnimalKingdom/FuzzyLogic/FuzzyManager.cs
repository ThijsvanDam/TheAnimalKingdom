using TheAnimalKingdom.FuzzyLogic.FuzzyTerms;

namespace TheAnimalKingdom.FuzzyLogic
{
    public static class FuzzyManager
    {
        public static double CalculateDesirability(FuzzyModule fm, double distance, double hunger, string desirability)
        {
            fm.Fuzzify("Hunger", hunger);
            fm.Fuzzify("DistanceToEnemy", distance);

            return fm.Defuzzify(desirability, DefuzzifyMethod.MaxAV);
        }
        
        public static FuzzyModule CreateBaseGazelleModule()
        {
            FuzzyModule gazelleFuzzyModule = new FuzzyModule();
            
            // Antecedent
            FuzzyVariable hunger = gazelleFuzzyModule.CreateFLV("Hunger");
            hunger.AddLeftShoulderSet("Full", 0, 50, 75);
            hunger.AddTriangularSet("CouldEat", 50, 75, 100);
            hunger.AddRightShoulderSet("Hungry", 75, 100, 150);


            FuzzyVariable distanceToEnemy = gazelleFuzzyModule.CreateFLV("DistanceToEnemy");
            distanceToEnemy.AddLeftShoulderSet("Close", 0, 100, 150);
            distanceToEnemy.AddTriangularSet("Middle", 100, 150, 200);
            distanceToEnemy.AddRightShoulderSet("Far", 150, 200, 600);
            
            return gazelleFuzzyModule;
        }

        public static FuzzyModule GazelleWannaRun(FuzzyModule gazelleFuzzyModule)
        {
            // Get the antecedents
            FuzzyVariable hunger = gazelleFuzzyModule.GetVariable("Hunger");
            FuzzyTermSet Full = new FuzzyTermSet(hunger.MemberSets["Full"]);
            FuzzyTermSet CouldEat = new FuzzyTermSet(hunger.MemberSets["CouldEat"]);
            FuzzyTermSet Hungry = new FuzzyTermSet(hunger.MemberSets["Hungry"]);
            
            FuzzyVariable distanceToEnemy = gazelleFuzzyModule.GetVariable("DistanceToEnemy");
            FuzzyTermSet Close = new FuzzyTermSet(distanceToEnemy.MemberSets["Close"]);
            FuzzyTermSet Middle = new FuzzyTermSet(distanceToEnemy.MemberSets["Middle"]);
            FuzzyTermSet Far = new FuzzyTermSet(distanceToEnemy.MemberSets["Far"]);
            
            // Create the consequent
            FuzzyVariable run = gazelleFuzzyModule.CreateFLV("RunDesirability");
            
            FuzzyTermSet Undesirable = run.AddLeftShoulderSet("Undesirable", 0, 10, 20);
            FuzzyTermSet Desirable = run.AddTrapezoidSet("Desirable", 10, 20, 30, 50);
            FuzzyTermSet VeryDesirable = run.AddRightShoulderSet("VeryDesirable", 30, 50, 150);
            
            // We need 9 of these with HUNGER + DISTANCE = DESIRABILITY
            //          Close      Middle     Far
            // Full       VD         VD        UD
            // CouldEat   VD         D         UD
            // Hungry     VD         D         UD 
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Close), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Middle), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Far), Undesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Close), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Middle), Desirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Far), Undesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Close), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Middle), Desirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Far), Undesirable);

            return gazelleFuzzyModule;
        }

        public static FuzzyModule GazelleWannaEat(FuzzyModule gazelleFuzzyModule)
        {
            // Get the antecedents
            FuzzyVariable hunger = gazelleFuzzyModule.GetVariable("Hunger");
            FuzzyTermSet Full = new FuzzyTermSet(hunger.MemberSets["Full"]);
            FuzzyTermSet CouldEat = new FuzzyTermSet(hunger.MemberSets["CouldEat"]);
            FuzzyTermSet Hungry = new FuzzyTermSet(hunger.MemberSets["Hungry"]);
            
            FuzzyVariable distanceToEnemy = gazelleFuzzyModule.GetVariable("DistanceToEnemy");
            FuzzyTermSet Close = new FuzzyTermSet(distanceToEnemy.MemberSets["Close"]);
            FuzzyTermSet Middle = new FuzzyTermSet(distanceToEnemy.MemberSets["Middle"]);
            FuzzyTermSet Far = new FuzzyTermSet(distanceToEnemy.MemberSets["Far"]);
            
            // Create the consequent
            FuzzyVariable graze = gazelleFuzzyModule.CreateFLV("EatDesirability");
            
            FuzzyTermSet Undesirable = graze.AddLeftShoulderSet("Undesirable", 0, 10, 20);
            FuzzyTermSet Desirable = graze.AddTrapezoidSet("Desirable", 10, 20, 50, 60);
            FuzzyTermSet VeryDesirable = graze.AddRightShoulderSet("VeryDesirable", 50, 60, 150);
            
            // We need 9 of these with HUNGER + DISTANCE = DESIRABILITY
            //          Close      Middle     Far
            // Full       UD         UD        D
            // CouldEat   UD         D         VD
            // Hungry     UD         VD        VD 
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Middle), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Far), Desirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Middle), Desirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Far), VeryDesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Middle), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Far), VeryDesirable);

            return gazelleFuzzyModule;
        }
    }
}