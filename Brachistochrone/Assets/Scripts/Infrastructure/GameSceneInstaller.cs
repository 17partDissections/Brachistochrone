using System;
using Zenject;
using UnityEngine;
using InputSystem;
using System.Collections;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Language _language;
    [SerializeField] private AudioHandler _audioHandler;

    public override void InstallBindings()
    {
        BindGrid();
        BindLanguage();
        BindAudioHandler();
        BindEventBus();
        BindInputSystem();

    }



    public override void Start()
    {
        Container.Resolve<SceneGrid>().CreateNode(200,200);
        Container.Resolve<InputSystem_Actions>().Enable();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(QWERTYU());
    }
    private IEnumerator QWERTYU()
    {
        yield return new WaitForSeconds(3f);
        int randomX = UnityEngine.Random.Range(0, 4000);
        int resultX = UnityEngine.Mathf.FloorToInt(randomX / 20);
        int randomZ = UnityEngine.Random.Range(0, 4000);
        int resultZ = UnityEngine.Mathf.FloorToInt(randomZ / 20);
        Debug.Log($"randomX: {randomX}, randomZ: {randomZ}");
        Container.Resolve<SceneGrid>().GetNode($"x{resultX}z{resultZ}");

    }
    private void BindGrid()
    {
        Container
            .Bind<SceneGrid>()
            .FromNew()
            .AsSingle()
            .NonLazy();
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
