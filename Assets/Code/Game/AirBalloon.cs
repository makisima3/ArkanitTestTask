using System;
using System.Collections.Generic;
using Code.Game.AirBalloonStateMachine;
using Code.Game.AirBalloonStateMachine.Enums;
using Code.Game.Obstacles;
using Code.Player.Configs;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Code.Game
{
    [RequireComponent(typeof(BalloonStateMachine))]
    public class AirBalloon : MonoBehaviour
    {
        [Inject] private PlayerActionConfig playerActionConfig;
        [Inject] private PathsManager pathsManager;
        
        private BalloonStateMachine _stateMachine;
        
        private int _currentLineIndex;
        

        public float TargetXPosition { get; private set; }
        public int SwipeDirection { get; private set; }
        public UnityEvent OnDead { get; private set; }
        public UnityEvent OnStart { get; private set; }
        public BalloonStateMachine StateMachine => _stateMachine;

        private void Awake()
        {
            _stateMachine = GetComponent<BalloonStateMachine>();
            OnDead = new UnityEvent();
            OnStart = new UnityEvent();
        }

        private void Start()
        {
            Restart();
        }

        public void Restart()
        {
            _currentLineIndex = pathsManager.MiddlePathIndex;
            pathsManager.TryGetPath(_currentLineIndex, out var x);
            transform.position = new Vector3(x, transform.position.y, 0f);
            
            OnStart.Invoke();
        }

        public void Move(int direction)
        {
            var nextIndex = _currentLineIndex + direction;
            if(!pathsManager.TryGetPath(nextIndex, out var x))
                return;

            _currentLineIndex = nextIndex;
            TargetXPosition = x;
            SwipeDirection = direction;
            _stateMachine.CurrentState = BalloonStates.Moving;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(playerActionConfig.GodMod)
                return;
            
            if (other.TryGetComponent<Obstacle>(out var obstacle))
            {
                if(obstacle.PathLine == _currentLineIndex)
                    OnDead.Invoke();
            }
        }
    }
}