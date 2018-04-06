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
            distanceToEnemy.AddLeftShoulderSet("Close", 0, 60, 80);
            distanceToEnemy.AddTrapezoidSet("Middle", 60, 80, 110, 130);
            distanceToEnemy.AddRightShoulderSet("Far", 110, 130, 150);
            
            return gazelleFuzzyModule;
        }
        
        
        
        public static FuzzyModule GazelleWannaWander(FuzzyModule gazelleFuzzyModule)
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
            FuzzyVariable wander = gazelleFuzzyModule.CreateFLV("WanderDesirability");
            
            FuzzyTermSet Undesirable = wander.AddLeftShoulderSet("Undesirable", 0, 30, 40);
            FuzzyTermSet Desirable = wander.AddTrapezoidSet("Desirable", 30, 40, 60, 70);
            FuzzyTermSet VeryDesirable = wander.AddRightShoulderSet("VeryDesirable", 60, 70, 150);
            
            // We need 9 of these with HUNGER + DISTANCE = DESIRABILITY
            //          Close      Middle     Far
            // Full       UD         VD        VD
            // CouldEat   UD         D         VD
            // Hungry     UD         UD        UD 
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Middle), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Far), VeryDesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Middle), Desirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Far), VeryDesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Middle), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Far), Undesirable);

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
            
            FuzzyTermSet Undesirable = run.AddLeftShoulderSet("Undesirable", 0, 30, 40);
            FuzzyTermSet Desirable = run.AddTrapezoidSet("Desirable", 30, 40, 60, 70);
            FuzzyTermSet VeryDesirable = run.AddRightShoulderSet("VeryDesirable", 60, 70, 150);
            
            // We need 9 of these with HUNGER + DISTANCE = DESIRABILITY
            //          Close      Middle     Far
            // Full       VD         D         UD
            // CouldEat   VD         UD        UD
            // Hungry     VD         UD        UD 
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Close), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Middle), Desirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Far), Undesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Close), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Middle), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Far), Undesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Close), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Middle), Undesirable);
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
            
            FuzzyTermSet Undesirable = graze.AddLeftShoulderSet("Undesirable", 0, 30, 40);
            FuzzyTermSet Desirable = graze.AddTrapezoidSet("Desirable", 30, 40, 60, 70);
            FuzzyTermSet VeryDesirable = graze.AddRightShoulderSet("VeryDesirable", 60, 70, 150);
            
            // We need 9 of these with HUNGER + DISTANCE = DESIRABILITY
            //          Close      Middle     Far
            // Full       UD         UD        UD
            // CouldEat   UD         D         D
            // Hungry     UD         VD        VD 
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Middle), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Full, Far), Undesirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Middle), Desirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(CouldEat, Far), Desirable);
            
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Close), Undesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Middle), VeryDesirable);
            gazelleFuzzyModule.AddRule(new FuzzyTermAND(Hungry, Far), VeryDesirable);

            return gazelleFuzzyModule;
        }
    }
}