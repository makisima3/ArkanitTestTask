﻿using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Utils
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class AdaptiveGrid : MonoBehaviour
    {
        [SerializeField] private Vector2 gridSize = new Vector2(5, 3);
        
        private GridLayoutGroup _gridLayoutGroup;
        private RectTransform _rectTransform;
        private float _width;
        private float _heith;
        
        private void Start()
        {
            Init();
            SetSize();
        }

        private void Update()
        {
            SetSize();
        }

        //Only for OnValidate
        private void Init()
        {
            _gridLayoutGroup = GetComponent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void OnValidate()
        {
            Init();
            SetSize();
        }

        [ContextMenu("SetSize")]
        private void SetSize()
        {
            _width = _rectTransform.rect.width;
            _heith = _rectTransform.rect.height;
            
            var cellSize = new Vector2(_width / gridSize.x, _width / gridSize.x);
            _gridLayoutGroup.cellSize = cellSize;
        }
    }
}