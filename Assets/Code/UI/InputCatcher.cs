using System;
using Code.Game;
using Code.Player.Configs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace Code.UI
{
    public class InputCatcher : MonoBehaviour,IDragHandler
    {
        [Inject] private PlayerActionConfig playerActionConfig;
        [Inject] private AirBalloon airBalloon;

        private bool _isInputAllowed;
        
        public UnityEvent<int> OnSwipe { get; private set; }

        private void Awake()
        {
            OnSwipe = new UnityEvent<int>();
            _isInputAllowed = true;
        }

        private void Start()
        {
            airBalloon.OnDead.AddListener(() => _isInputAllowed = false);
            airBalloon.OnStart.AddListener(() => _isInputAllowed = true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(!_isInputAllowed)
                return;
            
            if(Mathf.Abs(eventData.delta.x) < playerActionConfig.SwipeDeadZone)
                return;
            
            var direction = eventData.delta.x > 0 ? 1 : -1;
            OnSwipe.Invoke(direction);
        }
    }
}