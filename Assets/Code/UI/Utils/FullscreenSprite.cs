using UnityEngine;

namespace Code.Game.Views
{
    public class FullscreenSprite : MonoBehaviour
    {
        [SerializeField] private Camera targetCamera;
        private void Awake()
        {
            // Получаем ссылку на компонент SpriteRenderer
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

            if (spriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component not found on the GameObject.");
                return;
            }

            // Получаем размеры спрайта
            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            // Получаем размеры экрана в мировых координатах
            float screenHeight = targetCamera.orthographicSize * 2.0f;
            float screenWidth = screenHeight * Screen.width / Screen.height;

            // Масштабируем размеры спрайта так, чтобы они соответствовали размерам экрана
            Vector3 newScale = transform.localScale;
            newScale.x = screenWidth / spriteWidth;
            //newScale.y = screenHeight / spriteHeight;
            newScale.y = newScale.x;
            transform.localScale = newScale;
        }
    }
}