using Code.Game.AirBalloonStateMachine.Enums;
using Code.Game.AirBalloonStateMachine.States;
using Code.StateMachine;
using UnityEngine;

namespace Code.Game.AirBalloonStateMachine
{
    [RequireComponent(typeof(IdleBalloonState))]
    [RequireComponent(typeof(MovingBalloonState))]
    [RequireComponent(typeof(DeathBalloonState))]
    public class BalloonStateMachine : StateMachine<BalloonStates>
    {
        
    }
}