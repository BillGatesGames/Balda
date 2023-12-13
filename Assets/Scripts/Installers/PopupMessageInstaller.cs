using System.ComponentModel;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class PopupMessageInstaller : MonoInstaller
    {
        [SerializeField]
        private MessageView _messageView;

        public override void InstallBindings()
        {
            Container.Bind<IMessageModel>().To<MessageModel>().AsCached().When(c => c.ObjectType == typeof(PopupMessagePresenter));
            Container.Bind<IMessageView>().FromInstance(_messageView).AsCached().When(c => c.ObjectType == typeof(PopupMessagePresenter));
            Container.BindInterfacesAndSelfTo<PopupMessagePresenter>().AsSingle().WhenInjectedInto<IStateMachinePresenter>().NonLazy();
        }
    }
}
