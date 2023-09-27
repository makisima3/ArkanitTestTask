using DG.Tweening;
using UnityEngine;

namespace Code.Player.Configs
{
    
    [CreateAssetMenu(fileName = "PlayerActionConfig", menuName = "ScriptableObjects/Player/PlayerActionConfig", order = 0)]
    public class PlayerActionConfig : ScriptableObject
    {
        [SerializeField] private float swipeDeadZone = 10f;
        [SerializeField] private int linesCount = 3;
        [SerializeField] private float speed = 3f;
        [SerializeField] private bool godMod;
        
        [Space,Header("ControlSettings")]
        [SerializeField] private float timeToChangeLine = 0.5f;
        [SerializeField] private Ease changeLineEase = Ease.OutSine;

        public float SwipeDeadZone => swipeDeadZone;

        public int LinesCount => linesCount;

        public float TimeToChangeLine => timeToChangeLine;

        public Ease ChangeLineEase => changeLineEase;

        public float Speed => speed;

        public bool GodMod => godMod;

    }
}