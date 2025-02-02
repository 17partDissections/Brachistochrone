using System;
using Zenject;
using UnityEngine;
using System.Collections;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Language _language;
    [SerializeField] private AudioHandler _audioHandler;
    public int _sceneBorders = 50;
    [SerializeField] private Room _startMineRoom;

    public override void InstallBindings()
    {
        Container.BindInstance(_sceneBorders).WithId("SceneBorders");
        BindGrid();
        BindLanguage();
        BindAudioHandler();
        BindEventBus();
    }
    public void Awake()
    {
        SceneGrid grid = Container.Resolve<SceneGrid>();
        grid.CreateNode(_sceneBorders, _sceneBorders);
        grid.GetNode(grid.TranscodeNode(_startMineRoom.transform.position), out Node node);
        node.IsBusy = true; node.DoorUpBusy = true;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    //private IEnumerator QWERTYU()
    //{
    //    yield return new WaitForSeconds(3f);
    //    int randomX = UnityEngine.Random.Range(0, 4000);
    //    int resultX = UnityEngine.Mathf.FloorToInt(randomX / 20);
    //    int randomZ = UnityEngine.Random.Range(0, 4000);
    //    int resultZ = UnityEngine.Mathf.FloorToInt(randomZ / 20);
    //    Debug.Log($"randomX: {randomX}, randomZ: {randomZ}");
    //    //Container.Resolve<SceneGrid>().GetNode($"x{resultX}z{resultZ}");
    //}
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
}
