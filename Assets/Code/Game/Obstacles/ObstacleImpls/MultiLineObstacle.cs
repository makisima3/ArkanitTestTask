using System;
using System.Collections.Generic;
using Code.Player.Configs;
using DG.Tweening;
using Plugins.RobyyUtils;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Code.Game.Obstacles.ObstacleImpls
{
    public class MultiLineObstacle : Obstacle
    {
        [Inject] private PathsManager pathsManager;
        [Inject] private ObstacleSpawner obstacleSpawner;
        [Inject] private PlayerActionConfig playerActionConfig;

        [SerializeField] private float maxSpeed = 0.1f;
        [SerializeField] private float minSpeed = 0.7f;
        [SerializeField, Range(0f, 1f)] private float edgeToChangeLineMin = 0.4f;
        [SerializeField, Range(0f, 1f)] private float edgeToChangeLineMax = 0.8f;

        private Camera _camera;
        private float _spriteHeight;
        private float _cameraHeight;
        private float _targetYPosition;
        private float _speed;
        private float _startDistance;

        private bool _wasInitialized;
        private bool _move;

        private Tween _changeLineTween;
        private bool _isLineChanged;
        private int _anotherLineIndex;
        private float _edgeToChangeLine;

        public override void Init(int pathLine, ObstaclePool obstaclePool)
        {
            base.Init(pathLine, obstaclePool);

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

            var targetPosition = new Vector3(transform.position.x, _targetYPosition, 0f);
            _startDistance = Vector3.Distance(transform.position, targetPosition);

            var values = new List<int>();
            for (int i = 0; i < playerActionConfig.LinesCount; i++)
            {
                if (i != pathLine)
                    values.Add(i);
            }
            _anotherLineIndex = values.ChooseOne();
            _edgeToChangeLine = Random.Range(edgeToChangeLineMin, edgeToChangeLineMax);
            _isLineChanged = false;
            
            _wasInitialized = true;
        }

        private void OnPlayerDead()
        {
            _move = false;
        }

        private void Update()
        {
            if (!_wasInitialized || !_move)
                return;

            var targetPosition = new Vector3(transform.position.x, _targetYPosition, 0f);
            var direction = (targetPosition - transform.position).normalized;
            transform.position += direction * _speed * Time.deltaTime;

            var distanceToTarget = Vector3.Distance(transform.position, targetPosition);

            if (distanceToTarget <= _startDistance * _edgeToChangeLine && !_isLineChanged)
            {
                pathsManager.TryGetPath(_anotherLineIndex, out var x);
                transform.DOMoveX(x, playerActionConfig.TimeToChangeLine);
                _isLineChanged = true;
            }

            if (distanceToTarget <= 0.1f)
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