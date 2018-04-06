using System;
using System.Linq;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.FuzzyLogic;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThinkGazelle : CompositeGoal
    {
        private bool _running;
        private bool _eating;
        private bool _wandering;
        
        public GoalThinkGazelle(MovingEntity owner) : base(owner: owner, name: "Think")
        {
        }

        public override void Activate()
        {
            Status = Status.Active;
            Owner.SteeringBehaviours.ObstacleAvoidanceOn(1.0f);
            AddSubgoal(new GoalWander(Owner));
        }


        public override Status Process()
        {
            var enemy = Owner.IsScaredOf();

            ActivateIfInactive();


            double distanceBetweenAnimals = Owner.DistanceToClosestEnemy() / (6000 /150); // Could be 0 - 150
            double gazelleHunger = Owner.Hunger; // Could be 0 - 150


            FuzzyModule gazelle = FuzzyManager.CreateBaseGazelleModule();

            FuzzyManager.GazelleWannaRun(gazelle);
            double dWannaRun =
                FuzzyManager.CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "RunDesirability");

            FuzzyManager.GazelleWannaEat(gazelle);
            double dWannaEat =
                FuzzyManager.CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "EatDesirability");

            FuzzyManager.GazelleWannaWander(gazelle);
            double dWannaWander =
                FuzzyManager.CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "WanderDesirability");


            if (Owner.ID == 204 && false)
            {
                
                if(dWannaRun >= dWannaEat && dWannaRun >= dWannaWander)
                    Console.Write(@"[RUN]: ");
                
                else if(dWannaEat >=  dWannaRun && dWannaEat >= dWannaWander)
                    Console.Write(@"[EAT]: ");
                
                else if(dWannaWander >=  dWannaRun && dWannaWander >= dWannaEat)
                    Console.Write(@"[WAN]: ");
                else
                    Console.Write(@"[---]: ");
    //            
    
                Console.WriteLine("Run: " + dWannaRun + ", Eat: " + dWannaEat + ", Wander: " + dWannaWander);
            }

            if (!_running)
            {
                if (dWannaRun > dWannaEat && dWannaRun > dWannaWander)
                {
                    _running = true;
                    _eating = false;
                    _wandering = false;
                    RemoveAllSubgoals();
                    AddSubgoal(new GoalEscapeLion(Owner, enemy));
                }
            }

            if (!_eating)
            {
                if (dWannaEat > dWannaRun && dWannaEat > dWannaWander)
                {   
                    _eating = true;
                    _running = false;
                    _wandering = false;
                    RemoveAllSubgoals();
                    AddSubgoal(new GoalGatherFood(Owner));
                }
            }

            if (!_wandering)
            {
                if (dWannaWander > dWannaRun && dWannaWander > dWannaEat)
                {
                
                    _eating = false;
                    _running = false;
                    _wandering = true;
                    RemoveAllSubgoals();
                    AddSubgoal(new GoalWander(Owner));
                }
            } 

            var status = ProcessSubgoals();

            return status;
        }
    }
}