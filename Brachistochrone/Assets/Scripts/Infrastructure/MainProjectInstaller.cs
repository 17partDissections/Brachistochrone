using Q17pD.Brachistochrone;
using Zenject;

public class MainProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindMasterSave();
    }

    private void BindMasterSave()
    {
        Container
            .Bind<MasterSave>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}
