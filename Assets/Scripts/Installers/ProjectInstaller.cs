using Zenject;
using UnityEngine;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField] private CubeCollector _cubeCounter;
    [SerializeField] private ColorParser _colorParser;
    [SerializeField] private GoldHandler _goldHandler;
    [SerializeField] private SceneLoader _sceneLoader;

    public override void InstallBindings()
    {
        Container.Bind<CubeCollector>().FromComponentInNewPrefab(_cubeCounter).AsSingle().NonLazy();
        Container.Bind<ColorParser>().FromComponentInNewPrefab(_colorParser).AsSingle().NonLazy();
        Container.Bind<GoldHandler>().FromComponentInNewPrefab(_goldHandler).AsSingle().NonLazy();
        Container.Bind<SceneLoader>().FromComponentInNewPrefab(_sceneLoader).AsSingle().NonLazy();
    }
}
