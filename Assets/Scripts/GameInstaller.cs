using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CubeCounter _cubeCounter;
    [SerializeField] private ColorParser _colorParser;
    [SerializeField] private GoldHandler _goldHandler;

    public override void InstallBindings()
    {
        Container.Bind<CubeCounter>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<ColorParser>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<GoldHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
