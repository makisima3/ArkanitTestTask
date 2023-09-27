using System;
using System.Collections.Generic;
using Code.Player.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Game
{
    public class PathsManager : MonoBehaviour
    {
        [Inject] private PlayerActionConfig playerActionConfig;
        
        [SerializeField] private float lineWidthAdditional = 0.5f;

        private List<float> _linePositions;
        private Camera _camera;

        public int MiddlePathIndex => _linePositions.Count / 2;

        private void Awake()
        {
            _linePositions = new List<float>();
            _camera = Camera.main;

            float viewportWidth = _camera.orthographicSize * _camera.aspect;
            var lineSegment = viewportWidth / playerActionConfig.LinesCount + lineWidthAdditional;
            for (var i = 0; i < playerActionConfig.LinesCount; i++)
            {
                
                _linePositions.Add(lineSegment * (i + 1) - lineSegment / 2f - (lineSegment * (playerActionConfig.LinesCount)) /2f);
            }
        }

        public bool TryGetPath(int index, out float pathX)
        {
            pathX = 0;
            
            if (index < 0 || index >= _linePositions.Count)
                return false;

            pathX = _linePositions[index];
            return true;
        }
    }
}