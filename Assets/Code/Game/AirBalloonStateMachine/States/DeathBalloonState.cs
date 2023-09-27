using Code.Game.AirBalloonStateMachine.Enums;
using Code.StateMachine;
using UnityEngine;

namespace Code.Game.AirBalloonStateMachine.States
{
    public class DeathBalloonState : MonoBehaviour, IState<BalloonStates>
    {
        private AirBalloon _airBalloon;

        public BalloonStates Type => BalloonStates.Death;

        public void OnEnter()
        {
            if (_airBalloon == null)
                _airBalloon = GetComponent<AirBalloon>();
            
            _airBalloon.OnDead.Invoke();
        }

        public void OnExit()
        {

        }

        public void Loop()
        {

        }
    }
}