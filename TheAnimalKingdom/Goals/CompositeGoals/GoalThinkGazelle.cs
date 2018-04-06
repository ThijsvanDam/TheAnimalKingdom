using System;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.FuzzyLogic;
using TheAnimalKingdom.Goals.AtomicGoals;
using TheAnimalKingdom.Goals.Base;

namespace TheAnimalKingdom.Goals.CompositeGoals
{
    public class GoalThinkGazelle : CompositeGoal
    {
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


            double distanceBetweenAnimals = Owner.DistanceToClosestLion(); // Could be 0 - 600
            double gazelleHunger = Owner.Hunger; // Could be 0 - 150


            FuzzyModule gazelle = FuzzyManager.CreateBaseGazelleModule();

            FuzzyManager.GazelleWannaRun(gazelle);
            double dWannaRun =
                FuzzyManager.CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "RunDesirability");

            FuzzyManager.GazelleWannaEat(gazelle);
            double dWannaEat =
                FuzzyManager.CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "EatDesirability");


            if (dWannaEat > dWannaRun)
            {
                RemoveAllSubgoals();
                AddSubgoal(new GoalGatherFood(Owner));
            }
            else
            {
                RemoveAllSubgoals();
                AddSubgoal(new GoalEscapeLion(Owner, enemy));
            }

            var status = ProcessSubgoals();

            return status;
        }
    }
}