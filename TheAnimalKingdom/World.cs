using System;
using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.FuzzyLogic;
using TheAnimalKingdom.FuzzyLogic.FuzzySets;
using TheAnimalKingdom.FuzzyLogic.FuzzyTerms;
using TheAnimalKingdom.Util;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheAnimalKingdom.Behaviours;

namespace TheAnimalKingdom
{
    public class World
    {
        public static int Intensity = 0;
        public static int MouseX = 0;
        public static int MouseY = 0;
        public static bool GodMode { get; set; }

        public List<MovingEntity> Entities = new List<MovingEntity>();
        public List<ObstacleEntity> Obstacles = new List<ObstacleEntity>();
        public int Width { get; set; }
        public int Height { get; set; }

        public PathManager PathManager { get; }
        
        public SparseGraph graph;
        public FuzzyModule gazelle;

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            
            PathManager = new PathManager(numCyclesPerUpdate:200);
            _populate();

            GodMode = false;
        }

        public FuzzyModule CreateBaseLionModule()
        {
            FuzzyModule lionFuzzyModule = new FuzzyModule();
            
            // Antecedent
            FuzzyVariable hunger = lionFuzzyModule.CreateFLV("Hunger");
            hunger.AddLeftShoulderSet("Full", 0, 50, 75);
            hunger.AddTriangularSet("CouldEat", 50, 75, 100);
            hunger.AddRightShoulderSet("Hungry", 75, 100, 150);


            FuzzyVariable distanceToEnemy = lionFuzzyModule.CreateFLV("DistanceToFood");
            distanceToEnemy.AddLeftShoulderSet("Close", 0, 100, 150);
            distanceToEnemy.AddTriangularSet("Middle", 100, 150, 200);
            distanceToEnemy.AddRightShoulderSet("Far", 150, 200, 600);
            
            return lionFuzzyModule;
        }

        public FuzzyModule CreateBaseGazelleModule()
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


        public FuzzyModule GazelleWannaRun(FuzzyModule gazelleFuzzyModule)
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

        public FuzzyModule GazelleWannaEat(FuzzyModule gazelleFuzzyModule)
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

        private void _populate()
        {
            FillObstaclesWithArray(GetFunPlayField());
            graph = GraphGenerator.FloodFill(world: this, startPosition: new Vector2D(50f, 50f));
            GraphGenerator.SetNearestItems(this);
            
            Lion l1 = new Lion(new Vector2D(50f, 50f), this);
            Lion l2 = new Lion(new Vector2D(60f, 60f), this);
            
            Gazelle g1 = new Gazelle(new Vector2D(200f, 200f), this);
            Gazelle g2 = new Gazelle(new Vector2D(250f, 250f), this);
 
            Entities.AddRange(new List<MovingEntity>()
            {
                g1, l1, g2, l2
            });
            
            
            
            double distanceBetweenAnimals = 140; // Could be 0 - 150
            double gazelleHunger = 80; // Could be 0 - 150


            gazelle = CreateBaseGazelleModule();

            GazelleWannaRun(gazelle);
            double dWannaRun = CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "RunDesirability");
            
            GazelleWannaEat(gazelle);
            double dWannaEat = CalculateDesirability(gazelle, distanceBetweenAnimals, gazelleHunger, "EatDesirability");

        }
        
        public double CalculateDesirability(FuzzyModule fm, double distance, double hunger, string desirability)
        {
            fm.Fuzzify("Hunger", hunger);
            fm.Fuzzify("DistanceToEnemy", distance);

            return fm.Defuzzify(desirability, DefuzzifyMethod.MaxAV);
        }
        
        public void Update(float timeElapsed)
        {
            PathManager.UpdateSearches();
    
            foreach (BaseGameEntity entity in Entities)
            {
                entity.Update(timeElapsed);
                if (entity.ID == 0)
                {
                    entity.VPos.X = MouseX;
                    entity.VPos.Y = MouseY;
                }
            }
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
                        Obstacles.Add(new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this));
                    }
                    if (array[i, j] == 2)
                    {
                        SquaredObstacle a = new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this, ItemType.Grass);
                        Obstacles.Add(a);
                    }
                    if (array[i, j] == 3)
                    {
                        SquaredObstacle a = new SquaredObstacle(
                            new Vector2D((fullSize * j) + (25f / 2f), (fullSize * i) + (fullSize / 2)), 1f, this, ItemType.Water);
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
                {  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1  }, // 1
                {  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  2,  2,  2,  2,  1  }, // 2 
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  2,  2,  2,  1  }, // 3
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  2,  1  }, // 4
                {  1,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 5
                {  1,  1,  1,  1,  1,  1,  1,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 6
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 7
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 8
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 9
                {  1,  0,  0,  0,  0,  1,  0,  0,  0,  0,  0,  2,  2,  2,  1,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 10
                {  1,  0,  0,  0,  2,  1,  0,  0,  0,  0,  2,  2,  2,  2,  1,  0,  0,  0,  0,  1,  1,  0,  0,  1  }, // 11
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 12
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 13
                {  1,  0,  0,  0,  1,  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 14
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  2,  2,  2,  0,  0,  0,  0,  0,  0,  0,  0,  1  }, // 15
                {  1,  0,  0,  0,  1,  1,  1,  1,  1,  1,  0,  0,  2,  1,  2,  2,  0,  0,  0,  0,  0,  0,  0,  1  }, // 16
                {  1,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  0,  2,  1,  1,  2,  2,  0,  0,  0,  0,  0,  0,  1  }, // 17
                {  1,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  1  }, // 18
                {  1,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  3,  1  }, // 19
                {  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1  }, // 20
            };
        }

        public void Render(Graphics g)
        {
            if (GodMode)
            {
                graph.Render(g);
            }

            foreach (ObstacleEntity obstacleEntity in Obstacles)
            {
                obstacleEntity.Render(g);
            }
            foreach (BaseGameEntity baseGameEntity in Entities)
            {
                baseGameEntity.Render(g);
            }
        }
    }
}
