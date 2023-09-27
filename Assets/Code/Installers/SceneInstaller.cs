using Code.Game;
using Code.Game.Obstacles;
using Code.Player.Configs;
using Code.Player.Data;
using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerDataHolder playerDataHolder;
        [SerializeField] private InputCatcher inputCatcher;
        [SerializeField] private PathsManager pathsManager;
        [SerializeField] private BackMovement backMovement;
        [SerializeField] private ObstacleSpawner obstacleSpawner;
        [SerializeField] private AirBalloon airBalloon;
        
        [Space, Header("Configs")]
        [SerializeField] private PlayerDataConfig playerDataConfig;
        [SerializeField] private PlayerActionConfig playerActionConfig;
        [SerializeField] private ObstaclesConfig obstaclesConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerDataHolder>().FromInstance(playerDataHolder).AsSingle().NonLazy();
            Container.Bind<InputCatcher>().FromInstance(inputCatcher).AsSingle().NonLazy();
            Container.Bind<PathsManager>().FromInstance(pathsManager).AsSingle().NonLazy();
            Container.Bind<BackMovement>().FromInstance(backMovement).AsSingle().NonLazy();
            Container.Bind<ObstacleSpawner>().FromInstance(obstacleSpawner).AsSingle().NonLazy();
            Container.Bind<AirBalloon>().FromInstance(airBalloon).AsSingle().NonLazy();

            //Configs
           Container.Bind<PlayerDataConfig>().FromInstance(playerDataConfig).AsSingle().NonLazy();
           Container.Bind<PlayerActionConfig>().FromInstance(playerActionConfig).AsSingle().NonLazy();
           Container.Bind<ObstaclesConfig>().FromInstance(obstaclesConfig).AsSingle().NonLazy();
        }
    }
}