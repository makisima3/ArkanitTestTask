using System;
using Plugins.RobyyUtils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Game.Obstacles.ObstacleImpls
{
    public class BaseObstacle : Obstacle
    {
        [Inject] private PathsManager pathsManager;
        [Inject] private ObstacleSpawner obstacleSpawner;

        [SerializeField] private float maxSpeed = 0.1f;
        [SerializeField] private float minSpeed = 0.7f;
        
        private Camera _camera;
        private float _spriteHeight;
        private float _cameraHeight;
        private float _targetYPosition;
        private float _speed;

        private bool _wasInitialized;
        private bool _move;

        public override void Init(int pathLine,ObstaclePool obstaclePool)
        {
            base.Init(pathLine,obstaclePool);

            _camera = Camera.main;

            _spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
            _cameraHeight = _camera.orthographicSize * 2f;

            pathsManager.TryGetPath(PathLine, out var pathXPos);
            var y = _camera.transform.position.y + _cameraHeight / 2f + _spriteHeight / 2f;
            var x = pathXPos;
            transform.position = new Vector3(x, y, 0f);

            _targetYPosition = _camera.transform.position.y - _cameraHeight / 2f - _spriteHeight / 2f;
            _speed = Random.Range(minSpeed, maxSpeed);

            airBalloon.OnDead.AddListener(OnPlayerDead);
            _move = true;
            
            _wasInitialized = true;
        }

        private void OnPlayerDead()
        {
            _move = false;
        }

        private void Update()
        {
            if(!_wasInitialized || !_move)
                return;

            var targetPosition = new Vector3(transform.position.x, _targetYPosition, 0f);
            var direction = (targetPosition - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, targetPosition) <= 0.1f) 
                _obstaclePool.ReturnObject(this);
        }

        private void OnDisable()
        {
            _wasInitialized = false;
            obstacleSpawner.RemoveObstacle();
            airBalloon.OnDead?.RemoveListener(OnPlayerDead);
            airBalloon.OnStart?.RemoveListener(OnStart);
        }
    }
}