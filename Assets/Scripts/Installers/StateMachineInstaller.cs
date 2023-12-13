using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class StateMachineInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStateMachineModel>().To<StateMachineModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<StateMachinePresenter>().AsSingle();
        }
    }
}
