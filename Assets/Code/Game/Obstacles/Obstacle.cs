using System.Collections.Generic;
using Code.Game.Obstacles.ObstacleImpls;
using Plugins.RobyyUtils;
using UnityEngine;
using Zenject;

namespace Code.Game.Obstacles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Obstacle : MonoBehaviour
    {
        [Inject] protected AirBalloon airBalloon;
        
        [SerializeField] private List<Sprite> sprites;

        protected ObstaclePool _obstaclePool;
        
        public int PathLine { get; private set; }
        
        public virtual void Init(int pathLine,ObstaclePool obstaclePool)
        {
            PathLine = pathLine;
            _obstaclePool = obstaclePool;
            
            GetComponent<SpriteRenderer>().sprite = sprites.ChooseOne();
            airBalloon.OnStart.AddListener(OnStart);
        }

        protected void OnStart()
        {
            _obstaclePool.ReturnObject(this);
        }
    }
}