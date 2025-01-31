using System;
using Zenject;
using UnityEngine;

public class MenuSceneInstaller : MonoInstaller
{
    [SerializeField] private AudioHandler _audioHandler;

    public override void InstallBindings()
    {
        BindAudioHandler();
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
