using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class AlphabetInstaller : MonoInstaller
    {
        [SerializeField]
        private AlphabetView _alphabetView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AlphabetModel>().AsSingle();
            Container.Bind<IAlphabetView>().FromInstance(_alphabetView).AsSingle();
            Container.BindInterfacesAndSelfTo<AlphabetPresenter>().AsSingle();
        }
    }
}
