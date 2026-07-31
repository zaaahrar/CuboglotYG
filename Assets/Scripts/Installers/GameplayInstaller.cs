using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private FallHandler _fallHandler;

    public override void InstallBindings()
    {
        Container.Bind<FallHandler>().FromComponentInNewPrefab(_fallHandler).AsSingle().NonLazy();
    }
}
