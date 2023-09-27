using Code.Game.AirBalloonStateMachine.Enums;
using Code.Player.Configs;
using Code.StateMachine;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Game.AirBalloonStateMachine.States
{
    public class MovingBalloonState : MonoBehaviour, IState<BalloonStates>
    {
        [Inject] private PlayerActionConfig playerActionConfig;

        [SerializeField] private float rotationAngle = 15f;
        
        private AirBalloon _airBalloon;
        private Tween _moveTween;
        private Tween _rotationTween;
        
        public BalloonStates Type => BalloonStates.Moving;
        
        public void OnEnter()
        {
            if (_airBalloon == null)
                _airBalloon = GetComponent<AirBalloon>();

            _moveTween = transform
                .DOMoveX(_airBalloon.TargetXPosition, playerActionConfig.TimeToChangeLine)
                .SetEase(playerActionConfig.ChangeLineEase)
                .OnComplete(() => _airBalloon.StateMachine.CurrentState = BalloonStates.Idle);

            _rotationTween = transform
                .DORotate(new Vector3(0f, 0f, rotationAngle * _airBalloon.SwipeDirection * -1), playerActionConfig.TimeToChangeLine /3f)
                .SetEase(playerActionConfig.ChangeLineEase)
                .OnComplete(() =>
                {
                    _rotationTween = transform
                        .DORotate(Vector3.zero, playerActionConfig.TimeToChangeLine / 3f)
                        .SetEase(playerActionConfig.ChangeLineEase);
                });
        }

        public void OnExit()
        {
            _moveTween.Kill();
            _rotationTween.Kill();
        }

        public void Loop()
        {
           
        }
    }
}