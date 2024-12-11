using System;
using Zenject;
using UnityEngine;
using InputSystem;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Language _language;
    [SerializeField] private AudioHandler _audioHandler;

    public override void InstallBindings()
    {
        BindLanguage();
        BindAudioHandler();
        BindEventBus();
        BindInputSystem();

    }



    public override void Start()
    {
        Container.Resolve<InputSystem_Actions>().Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void BindLanguage()
    {
        Container
            .Bind<Language>()
            .FromInstance(_language)
            .AsSingle()
            .NonLazy();
    }
    private void BindAudioHandler()
    {
        Container
            .Bind<AudioHandler>()
            .FromInstance(_audioHandler)
            .AsSingle()
            .NonLazy();
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
