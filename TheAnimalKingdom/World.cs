using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheAnimalKingdom.Behaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.FuzzyLogic;
using TheAnimalKingdom.FuzzyLogic.FuzzySets;
using TheAnimalKingdom.FuzzyLogic.FuzzyTerms;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom
{
    public class World
    {
        public static int Intensity = 0;
        public static int MouseX = 0;
        public static int MouseY = 0;
        public static bool ShouldRenderGraph { get; set; }

        public List<BaseGameEntity> Entities = new List<BaseGameEntity>();
        public List<ObstacleEntity> Obstacles = new List<ObstacleEntity>();
        public int Width { get; set; }
        public int Height { get; set; }

        private AStarSearch _aStarSearch;

        private FuzzyModule fm;

        public SparseGraph graph;

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            _populate();
            graph = GraphGenerator.FloodFill(world: this, startPosition: new Vector2D(7.5f, 7.5f));

            ShouldRenderGraph = false;
        }

        private void _populate()
        {
           
            fm = new FuzzyModule();
            
            // Antecedent
            FuzzyVariable hunger = fm.CreateFLV("Hunger");
            hunger.AddLeftShoulderSet("Full", 0, 50, 75);
            hunger.AddTriangularSet("CouldEat", 50, 75, 100);
            hunger.AddRightShoulderSet("Hungry", 75, 100, 150);

            FuzzyTermSet Full = new FuzzyTermSet(hunger.MemberSets["Full"]);
            FuzzyTermSet CouldEat = new FuzzyTermSet(hunger.MemberSets["CouldEat"]);
            FuzzyTermSet Hungry = new FuzzyTermSet(hunger.MemberSets["Hungry"]);
            
            FuzzyVariable thirst = fm.CreateFLV("Thirst");
            thirst.AddLeftShoulderSet("Saturated", 0, 25, 50);
            thirst.AddTriangularSet("CouldDrink", 25, 50, 75);
            thirst.AddRightShoulderSet("Thirsty", 50, 75, 150);
            
            FuzzyTermSet Saturated = new FuzzyTermSet(thirst.MemberSets["Saturated"]);
            FuzzyTermSet CouldDrink = new FuzzyTermSet(thirst.MemberSets["CouldDrink"]);
            FuzzyTermSet Thirsty = new FuzzyTermSet(thirst.MemberSets["Thirsty"]);

//            FuzzyVariable distance = fm.CreateFLV("Distance");
//            distance.AddLeftShoulderSet("Close", 0, 25, 50);
//            distance.AddTriangularSet("Middle", 25, 50, 75);
//            distance.AddRightShoulderSet("Far", 50, 75, 150);
//            
//            FuzzyTermSet Close = new FuzzyTermSet(distance.MemberSets["Close"]);
//            FuzzyTermSet Middle = new FuzzyTermSet(distance.MemberSets["Middle"]);
//            FuzzyTermSet Far = new FuzzyTermSet(distance.MemberSets["Far"]);
            
            // Consequent
            FuzzyVariable desirability = fm.CreateFLV("Desirability");
            desirability.AddLeftShoulderSet("Undesirable", 0, 10, 20);
            desirability.AddTrapezoidSet("Desirable", 10, 20, 50, 60);
            desirability.AddRightShoulderSet("VeryDesirable", 50, 60, 150);
            
            FuzzyTermSet Undesirable = new FuzzyTermSet(desirability.MemberSets["Undesirable"]);
            FuzzyTermSet Desirable = new FuzzyTermSet(desirability.MemberSets["Desirable"]);
            FuzzyTermSet VeryDesirable = new FuzzyTermSet(desirability.MemberSets["VeryDesirable"]);
            
            // We need 9 of these with HUNGER + THIRST = DESIRABILITY
            //          Saturated CouldDrink Thirsty
            // Full       VD         VD         D
            // CouldEat   D          D         UD
            // Hungry     UD         UD        UD 
            
            fm.AddRule(new FuzzyTermAND(Full, Saturated), VeryDesirable);
            fm.AddRule(new FuzzyTermAND(Full, CouldDrink), VeryDesirable);
            fm.AddRule(new FuzzyTermAND(Full, Thirsty), Desirable);
            
            fm.AddRule(new FuzzyTermAND(CouldEat, Saturated), Desirable);
            fm.AddRule(new FuzzyTermAND(CouldEat, CouldDrink), Desirable);
            fm.AddRule(new FuzzyTermAND(CouldEat, Thirsty), Undesirable);
            
            fm.AddRule(new FuzzyTermAND(Hungry, Saturated), Undesirable);
            fm.AddRule(new FuzzyTermAND(Hungry, CouldDrink), Undesirable);
            fm.AddRule(new FuzzyTermAND(Hungry, Thirsty), Undesirable);
            double val = CalculateDesirability(fm, 10, 60);

            Console.WriteLine("Val: " + val);
            


//            StaticEntity s1 = new StaticEntity(new Vector2D(0, 0), this); // the entity with ID=0 always follow the cursor so it has to be at the start.
//            Gazelle g1 = new Gazelle(new Vector2D(0, 0), this);
//            //            Gazelle g2 = new Gazelle(new Vector2D(30, 20), this);
//            //            Gazelle g3 = new Gazelle(new Vector2D(25, 10), this);
//
//            g1.SteeringBehaviours.ArriveOn(s1, 2);
////            g1.SteeringBehaviours.WanderOn(1);
//            g1.SteeringBehaviours.ObstacleAvoidanceOn(1);
//
//            Entities.AddRange(new List<BaseGameEntity>()
//            {
//                s1, g1,
//            });
//
//            FillObstaclesWithArray(GetFunPlayField());
        }

        public double CalculateDesirability(FuzzyModule fm, double thirst, double hunger)
        {
            fm.Fuzzify("Thirst", thirst);
            fm.Fuzzify("Hunger", hunger);

             return fm.Defuzzify("Desirability", DefuzzifyMethod.MaxAV);
        }
        
        public void Update(float timeElapsed)
        {
//            foreach (BaseGameEntity entity in Entities)
//            {
//                entity.Update(timeElapsed);
//                if (entity.ID == 0)
//                {
//                    entity.VPos.X = MouseX;
//                    entity.VPos.Y = MouseY;
//                }
//            }
        }

        public void StartPathFollowing(int target)
        {
//            //ToDo: This needs to be more dynamic
//            var entity = (MovingEntity)Entities[1];
//            var source = graph.FindNearestNode(entity.VPos);
//            _aStarSearch = new AStarSearch(graph, source.Index, target);
//            entity.SteeringBehaviours.FollowPathOff();
//            entity.SteeringBehaviours.FollowPathOn(route: _aStarSearch.GetRoute(), intensity: 5.0);
            
        }

        private void FillObstaclesWithArray(int[,] array)
        {
            float fullSize = 25f;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == 1)
                    {
                        Obstacles.Add(new SquaredObstacle(new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this));
                    }
                    if (array[i, j] == 2)
                    {
                        SquaredObstacle a = new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this);
                        a.Color = Color.Red;
                        Obstacles.Add(a);
                    }
                }
            }
        }

        public int[,] GetFullWithObstaclesPlayField()
        {
            int[,] array = new int[20,24];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = 1;
                }
            }
            return array;
        }

        public int[,] GetFunPlayField()
        {
            return new int[20, 24]
            {//    1   2   3   4   5   6   7   8   9   10  11  12  13  14  15  16  17  18  19  20  21  22  23  24      
                {  0,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 1
                {  0,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 2 
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 3
                {  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 4
                {  1,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 5
                {  1,  0,  1,  1,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 6
                {  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 7
                {  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0  }, // 8
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  1,  1,  1,  0,  0  }, // 9
                {  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  0,  0,  0,  1,  1,  1,  0,  0  }, // 10
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  1,  1,  2,  2,  1,  1,  0,  0,  0,  1,  1,  1,  0,  0  }, // 11
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 12
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 13
                {  0,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1,  1,  1,  0,  0,  0,  0,  0  }, // 14
                {  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  1,  1,  1,  0,  0,  1,  1,  0,  0,  0,  0,  0  }, // 15
                {  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  1,  1,  1,  1,  0,  0,  1,  0,  0,  0,  0,  0  }, // 16
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0  }, // 17
                {  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0  }, // 18
                {  1,  1,  1,  1,  1,  0,  0,  0,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0  }, // 19
                {  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0  }, // 20
            };
        }

        public void ReadInputs()
        {
            Console.WriteLine("inputs");
        }
        
        public void Render(Graphics g)
        {
            
//            var.AddLeftShoulderSet("Low", 0, 45, 55);
//            var.AddTriangularSet("Medium", 45, 55, 65);
//            var.AddRightShoulderSet("High", 55, 65, 0);
            
//            fm.GetVariable("Hunger").Render(g, new Point(10, 10));
//            fm.GetVariable("Thirst").Render(g, new Point(10, 110));
//            fm.GetVariable("Distance").Render(g, new Point(10, 210));
//            fm.GetVariable("Desirability").Render(g, new Point(10, 310));
            
//            if (ShouldRenderGraph)
//            {
//                graph.Render(g);
//                _aStarSearch?.Render(g);
//            }
//
//            foreach (ObstacleEntity obstacleEntity in Obstacles)
//            {
//                obstacleEntity.Render(g);
//            }
//            foreach (BaseGameEntity baseGameEntity in Entities)
//            {
//                baseGameEntity.Render(g);
//            }
        }
    }
}
