using Code.Game.AirBalloonStateMachine.Enums;
using Code.StateMachine;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Game.AirBalloonStateMachine.States
{
    public class IdleBalloonState : MonoBehaviour, IState<BalloonStates>
    {
        [Inject] private InputCatcher inputCatcher;

        private AirBalloon _airBalloon;
        
        public BalloonStates Type => BalloonStates.Idle;
        
        public void OnEnter()
        {
            if (_airBalloon == null)
                _airBalloon = GetComponent<AirBalloon>();
            
            inputCatcher.OnSwipe.AddListener(_airBalloon.Move);
        }

        public void OnExit()
        {
            inputCatcher.OnSwipe.RemoveListener(_airBalloon.Move);
        }

        public void Loop()
        {
            
        }
    }
}