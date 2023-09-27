using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Code.Game.Obstacles.ObstacleImpls;
using Code.Player.Configs;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Game.Obstacles
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [Inject] private BackMovement backMovement;
        [Inject] private ObstaclesConfig obstaclesConfig;
        [Inject] private DiContainer container;
        [Inject] private PlayerActionConfig playerActionConfig;
        [Inject] private AirBalloon airBalloon;

        private Dictionary<float, ObstaclePool> _obstaclesPools;
        private int _obstaclesCount;
        private Coroutine _spawnCoroutine;
        
        public void Init()
        {
            _obstaclesPools = new Dictionary<float, ObstaclePool>();
            var step = backMovement.MaxYDistance / obstaclesConfig.Obstacles.Count;
            var counter = 1;

            foreach (var obstacle in obstaclesConfig.Obstacles)
            {
                var holder = new GameObject();
                holder.transform.SetParent(transform);
                holder.name = $"{obstacle.name}Holder";

                var pool = new ObstaclePool(obstacle, obstaclesConfig.MAXObstacleOnScreen, container, holder.transform);
                _obstaclesPools.Add(step * counter, pool);

                counter++;
            }
            
            airBalloon.OnDead.AddListener(StopSpawn);
            
            StartSpawn();
        }

        public void Reset()
        {
            StartSpawn();
        }

        private float FindClosestValue(float targetValue )
        {
            if (_obstaclesPools == null || _obstaclesPools.Count == 0)
            {
                Debug.LogError("The list of values is null or empty.");
                return 0f;
            }

            var closestValue = _obstaclesPools.First().Key;
            var closestDifference = Mathf.Abs(targetValue - closestValue);

            foreach (var value in _obstaclesPools)
            {
                var difference = Mathf.Abs(targetValue - value.Key);
                if (!(difference < closestDifference))
                    continue;
                closestValue = value.Key;
                closestDifference = difference;
            }

            return closestValue;
        }
        
        private void CreateObstacle()
        {
            var pool = _obstaclesPools[FindClosestValue(backMovement.CurrentYPosition)];
            var obstacle = pool.GetObject();
            obstacle.Init(Random.Range(0,playerActionConfig.LinesCount), pool);
            _obstaclesCount++;
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f / obstaclesConfig.SpawnRate);
                
                if(_obstaclesCount >= obstaclesConfig.MAXObstacleOnScreen)
                    continue;
                
                CreateObstacle();
            }
        }

        public void RemoveObstacle() => _obstaclesCount--;

        public void StartSpawn()
        {
            StopSpawn();
            _obstaclesCount = 0;
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        public void StopSpawn()
        {
            if(_spawnCoroutine == null)
                return;
            
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }
}