
using Zenject;
using UnityEngine;

public class TestSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEventBus();

    }



    public override void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void BindEventBus()
    {
        Container
            .Bind<EventBus>()
            .FromNew()
            .AsSingle()
            .NonLazy();

    }
}
