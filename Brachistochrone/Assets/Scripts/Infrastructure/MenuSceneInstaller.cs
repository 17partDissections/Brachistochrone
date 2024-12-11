using System;
using Zenject;
using UnityEngine;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] private Language _language;
    [SerializeField] private AudioHandler _audioHandler;

    public override void InstallBindings()
    {
        BindLanguage();
        BindAudioHandler();
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
}
