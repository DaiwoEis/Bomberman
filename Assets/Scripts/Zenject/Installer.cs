using UnityEngine;
using Zenject;

public class Installer : MonoInstaller<Installer>
{
    [SerializeField]
    private Map _map = null;

    public override void InstallBindings()
    {
        Container.Bind<Map>().FromInstance(_map);
    }
}