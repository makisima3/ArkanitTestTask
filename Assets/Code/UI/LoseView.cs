using System.Collections;
using System.Collections.Generic;
using Code.Game;
using Code.Game.Obstacles;
using Code.UI.Utils;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public class LoseView : ViewBase
    {
        [Inject] private AirBalloon airBalloon;
        [Inject] private BackMovement backMovement;
        [Inject] private ObstacleSpawner obstacleSpawner;
        
        
        [SerializeField] private TMP_Text text;
        [SerializeField] private List<Color> colors;
        [SerializeField] private int countDown = 3;

        private Tween _textTween;

        protected override void Start()
        {
            base.Start();
            
            airBalloon.OnDead.AddListener(() => Show());
        }

        protected override void OnShowBegin()
        {
            base.OnShowBegin();
            
            text.transform.localScale = Vector3.zero;
        }

        protected override void OnShowEnd()
        {
            base.OnShowEnd();

            StartCoroutine(CountDown());
        }

        private IEnumerator CountDown()
        {
            for (var i = 0; i < countDown; i++)
            {
                _textTween.Kill();
                text.transform.localScale = Vector3.zero;
                text.text = $"{countDown - i}";
                text.color = colors[i];

                text.transform.DOScale(Vector3.one, 1f);
                
                yield return new WaitForSeconds(1f);
            }
            
            Hide(true);
            Restart();
        }

        private void Restart()
        {
            airBalloon.Restart();
            backMovement.Restart();
            obstacleSpawner.StartSpawn();
        }
    }
}