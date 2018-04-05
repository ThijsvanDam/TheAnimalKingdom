using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using TheAnimalKingdom.Behaviours.AdvancedBehaviours;
using TheAnimalKingdom.Behaviours.BaseBehaviours;
using TheAnimalKingdom.Behaviours.NormalBehaviours;
using TheAnimalKingdom.Entities;
using TheAnimalKingdom.Util;

namespace TheAnimalKingdom.Behaviours
{
    public class SteeringBehaviours
    {
        #region NormalBehaviours

        private ArriveBehaviour _arrive;
        private FleeBehaviour _flee;
        private SeekBehaviour _seek;
        private WanderBehaviour _wander;
        private FollowPathBehaviour _followPath;

        private double _dArrive;
        private double _dFlee;
        private double _dSeek;
        private double _dWander;
        private double _dFollowPath;

        #endregion

        #region AdvancedBehaviours

        private ObstacleAvoidance _obstacleAvoidance;

        private double _dObstacleAvoidance;

        #endregion

        private MovingEntity _movingEntity;


        public SteeringBehaviours(MovingEntity movingEntity)
        {
            _movingEntity = movingEntity;
        }


        private bool _instanceExists(SteeringBehaviour behaviour)
        {
            return behaviour != null;
        }


        public Vector2D Calculate()
        {
            Vector2D sum = new Vector2D(0, 0);

            #region NormalBehaviours

            if (_instanceExists(_arrive))
            {
                Vector2D v = _arrive.Calculate().Multiply(_dArrive);
                sum.Add(v);
            }

            if (_instanceExists(_flee))
            {
                Vector2D v = _flee.Calculate().Multiply(_dFlee);
                sum.Add(v);
            }

            if (_instanceExists(_seek))
            {
                Vector2D v = _seek.Calculate().Multiply(_dSeek);
                sum.Add(v);
            }

            if (_instanceExists(_straightWalking))
            {
                Vector2D v = _straightWalking.Calculate().Multiply(_dStraightWalking);
                sum.Add(v);
            }

            if (_instanceExists(_wander))
            {
                Vector2D v = _wander.Calculate().Multiply(_dWander);
                sum.Add(v);
            }

            if (_instanceExists(_followPath))
            {
                Vector2D v = _followPath.Calculate().Multiply(_dFollowPath);
                sum.Add(v);
            }

            #endregion

            #region AdvancedBehaviours

            if (_instanceExists(_obstacleAvoidance))
            {
                Vector2D v = _obstacleAvoidance.Calculate().Multiply(_dObstacleAvoidance);
                sum.Add(v);
            }

            #endregion

            Vector2D a = sum.Truncate(_movingEntity.DMaxForce);
            return a;
        }


        #region NormalBehaviours

        public void WanderOn(double intensity)
        {
            _wander = new WanderBehaviour(_movingEntity);
            _dWander = intensity;
        }

        public void WanderOff()
        {
            _wander = null;
            _dWander = 0;
        }

        public void SeekOn(BaseGameEntity goal, double intensity)
        {
            _seek = new SeekBehaviour(_movingEntity, goal);
            _dSeek = intensity;
        }

        public void SeekOn(Vector2D destination, double intensity)
        {
            _seek = new SeekBehaviour(_movingEntity, destination);
            _dSeek = intensity;
        }

        public void SeekOff()
        {
            _seek = null;
            _dSeek = 0;
        }

        public void FleeOn(BaseGameEntity enemy, double intensity)
        {
            _flee = new FleeBehaviour(_movingEntity, enemy);
            _dFlee = intensity;
        }

        public void FleeOff()
        {
            _flee = null;
            _dFlee = 0;
        }

        public void ArriveOn(BaseGameEntity goal, double intensity)
        {
            _arrive = new ArriveBehaviour(_movingEntity, goal);
            _dArrive = intensity;
        }
        
        public void ArriveOn(Vector2D destination, double intensity)
        {
            _arrive = new ArriveBehaviour(_movingEntity, destination);
            _dArrive = intensity;
        }

        public void ArriveOff()
        {
            _arrive = null;
            _dArrive = 0;
        }

        public void FollowPathOn(List<NavGraphNode> route, double intensity)
        {
            _followPath = new FollowPathBehaviour(_movingEntity, route);
            _dFollowPath = intensity;
        }

        public void FollowPathOff()
        {
            _followPath = null;
            _dFollowPath = 0;
        }

        #endregion

        #region AdvancedBehaviours

        public void ObstacleAvoidanceOn(double intensity)
        {
            _obstacleAvoidance = new ObstacleAvoidance(_movingEntity);
            _dObstacleAvoidance = intensity;
        }

        public void ObstacleAvoidanceOff()
        {
            _obstacleAvoidance = null;
            _dObstacleAvoidance = 0;
        }

        #endregion

        public void DrawBehaviors(Graphics g)
        {
            #region NormalBehaviours

            if (_instanceExists(_arrive))
            {
                _arrive.DrawBehavior(g);
            }

            if (_instanceExists(_flee))
            {
                _flee.DrawBehavior(g);
            }

            if (_instanceExists(_seek))
            {
                _seek.DrawBehavior(g);
            }
            
            if (_instanceExists(_wander))
            {
                _wander.DrawBehavior(g);
            }

            #endregion

            #region AdvancedBehaviours

            if (_instanceExists(_obstacleAvoidance))
            {
                _obstacleAvoidance.DrawBehavior(g);
            }

            #endregion
        }
    }
}