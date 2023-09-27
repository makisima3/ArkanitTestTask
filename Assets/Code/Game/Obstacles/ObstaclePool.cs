using UnityEngine;
using Zenject;

namespace Code.Game.Obstacles
{
    public class ObstaclePool : Pooling.ObjectPool<Obstacle>
    {
        public ObstaclePool(Obstacle prefab, int initialSize, DiContainer container, Transform holder) : base(prefab, initialSize, container, holder)
        {
        }
    }
}