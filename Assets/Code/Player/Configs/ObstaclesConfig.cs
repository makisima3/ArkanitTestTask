using System.Collections.Generic;
using Code.Game.Obstacles;
using UnityEngine;

namespace Code.Player.Configs
{
    
    [CreateAssetMenu(fileName = "ObstaclesConfig", menuName = "ScriptableObjects/Player/ObstaclesConfig", order = 3)]
    public class ObstaclesConfig : ScriptableObject
    {
        [SerializeField] private List<Obstacle> obstacles;
        [SerializeField] private int maxObstacleOnScreen;
        [SerializeField] private float spawnRate = 2f;

        public float SpawnRate => spawnRate;

        public List<Obstacle> Obstacles => obstacles;

        public int MAXObstacleOnScreen => maxObstacleOnScreen;
    }
}