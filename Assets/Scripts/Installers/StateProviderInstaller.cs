using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class StateProviderInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStateProvider>().To<StateProvider>().AsSingle();
        }
    }
}
