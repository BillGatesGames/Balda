using UnityEngine;
using Zenject;

namespace Balda
{
    public class ResourcesLoaderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ResourceLoader>().AsSingle().NonLazy();
        }
    }
}