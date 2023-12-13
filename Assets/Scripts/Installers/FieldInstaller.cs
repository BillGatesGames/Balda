using UnityEngine;
using Zenject;

namespace Balda
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField]
        private FieldView _fieldView;

        [Inject]
        private IGameSettings _gameSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Trie>().AsSingle();
            Container.BindInstance(_gameSettings.Size);
            Container.BindInterfacesAndSelfTo<FieldModel>().AsSingle();
            Container.Bind<IFieldView>().FromInstance(_fieldView).AsSingle();
            Container.BindInterfacesAndSelfTo<FieldPresenter>().AsSingle();
        }
    }
}
