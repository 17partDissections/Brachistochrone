using Zenject;
using UnityEngine;
using Q17pD.Brachistochrone.Player;
using Q17pD.Brachistochrone.Player.Canvas;
using Q17pD.Brachistochrone;
using Q17pD.Brachistochrone.Factories;
using Q17pD.Brachistochrone.Enemy;
using System;

public class GameSceneInstaller : MonoInstaller
{
    //[SerializeField] private Saver.Setting _DesignerSetting;
    [SerializeField] private AudioHandler _audioHandler;
    public int _sceneBorders;
    [SerializeField] private Room _startMineRoom;
    [SerializeField] private ItemHolder _itemHolder;
    [SerializeField] private PlayerMovement _playerMovementInstance;
    [SerializeField] private Skinny _skinnyInstance;
    private LevelGeneration _levelGeneration;

    public override void InstallBindings()
    {
        Container.BindInstance(_sceneBorders).WithId("SceneBorders");
        BindPointerStorage();
        BindActionMap();
        BindGrid();
        BindAudioHandler();
        BindEventBus();
        BindItemHolder();
        BindObjectPool();
        BindPlayerMovement();
    }

    private void BindPlayerMovement()
    {
        Container
            .Bind<PlayerMovement>()
            .FromInstance(_playerMovementInstance)
            .AsSingle()
            .NonLazy();
    }

    private void BindObjectPool()
    {
        Q17pD.Brachistochrone.Factories.IFactory factory = Container.Instantiate<ItemFactory>();
        Container
            .Bind<ItemObjectPool>()
            .FromNew()
            .AsSingle()
            .WithArguments(factory,1)
            .Lazy();
    }

    private void BindItemHolder()
    {
        Container
            .Bind<ItemHolder>()
            .FromInstance(_itemHolder)
            .AsSingle()
            .Lazy();
    }

    private void BindPointerStorage()
    {
        Container
            .Bind<PointerStorage>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    private void BindActionMap()
    {
        Container
            .Bind<PlayerActionMap>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }

    public void Awake()
    {
        _levelGeneration = GetComponent<LevelGeneration>();
        SceneGrid grid = Container.Resolve<SceneGrid>();
        grid.CreateNode(_sceneBorders, _sceneBorders);
        grid.GetNode(grid.TranscodeVectorToNode(_startMineRoom.transform.position), out Node node);
        node.IsBusy = true; node.DoorUpBusy = true;
        Container.Inject(_levelGeneration, new object[] { _sceneBorders });
        Scenarist scenarist = Container.Instantiate<Scenarist>(new object[] { _skinnyInstance });
        Saver saver = Container.Instantiate<Saver>();
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
