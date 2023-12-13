using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class TopMessageInstaller : MonoInstaller
    {
        [SerializeField]
        private MessageView _messageView;

        public override void InstallBindings()
        {
            Container.Bind<IMessageModel>().To<MessageModel>().AsCached().When(c => c.ObjectType == typeof(TopMessagePresenter));
            Container.Bind<IMessageView>().FromInstance(_messageView).AsCached().When(c => c.ObjectType == typeof(TopMessagePresenter));
            Container.BindInterfacesAndSelfTo<TopMessagePresenter>().AsSingle();
        }
    }
}
