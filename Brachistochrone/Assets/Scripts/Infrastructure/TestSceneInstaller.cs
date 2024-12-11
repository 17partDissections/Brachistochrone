
using Zenject;
using UnityEngine;
using InputSystem;

public class TestSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindEventBus();
        BindInputSystem();

    }



    public override void Start()
    {
        Container.Resolve<InputSystem_Actions>().Enable();
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
    private void BindInputSystem()
    {
        Container
            .Bind<InputSystem_Actions>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}
