using Code.Game.Obstacles;
using Code.Player.Configs;
using UnityEngine;
using Zenject;

namespace Code.Game
{
    public class BackMovement : MonoBehaviour
    {
        [Inject] private PlayerActionConfig playerActionConfig;
        [Inject] private AirBalloon airBalloon;
        [Inject] private ObstacleSpawner obstacleSpawner;

        [SerializeField] private Camera targetCamera;

        private float _spriteHeight;
        private float _cameraHeight;

        private Vector3 _startPosition;
        private Vector3 _endPosition;
        private bool _move;
        
        public float CurrentYPosition => Mathf.Abs(transform.position.y - _spriteHeight / 2f);
        public float MaxYDistance { get; private set; }

        private void Start()
        {
            _spriteHeight = GetComponent<SpriteRenderer>().bounds.size.y;
            _cameraHeight = targetCamera.orthographicSize * 2f;

            SetStartPosition();
            SetEndPosition();

            MaxYDistance = Vector3.Distance(_startPosition, _endPosition) + targetCamera.orthographicSize;

            airBalloon.OnDead.AddListener(() => _move = false);

            _move = true;
            
            obstacleSpawner.Init();
        }

        private void Update()
        {
            if(!_move)
                return;
            
            transform.position = Vector3.Lerp(transform.position, _endPosition, playerActionConfig.Speed * Time.deltaTime);
        }

        [ContextMenu("SetStartPosition")]
        private void SetStartPosition()
        {
            var cameraPositionY = targetCamera.transform.position.y;
            var newYPosition = cameraPositionY - _cameraHeight / 2f + _spriteHeight / 2f;
            _startPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);
            transform.position = _startPosition;
        }

        [ContextMenu("SetEndPosition")]
        private void SetEndPosition()
        {
            var cameraPositionY = targetCamera.transform.position.y;
            var newYPosition = cameraPositionY + _cameraHeight / 2f - _spriteHeight / 2f;
            _endPosition = new Vector3(transform.position.x, newYPosition, transform.position.z);
        }

        public void Restart()
        {
            transform.position = _startPosition;
            _move = true;
        }
    }
}