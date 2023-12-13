using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField]
        private MenuView _menuView;

        public override void InstallBindings()
        {
            Container.Bind<IMenuView>().FromInstance(_menuView).AsSingle();
            Container.BindInterfacesAndSelfTo<MenuPresenter>().AsSingle();
        }
    }
}
