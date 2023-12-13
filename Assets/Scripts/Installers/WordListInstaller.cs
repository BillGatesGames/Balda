using ModestTree;
using System;
using UnityEngine;
using Zenject;

namespace Balda
{
    public class WordListInstaller : MonoInstaller
    {
        [SerializeField]
        private WordListView _leftWordListView;

        [SerializeField]
        private WordListView _rightWordListView;

        public override void InstallBindings()
        {
            var leftList = CreateWordList(PlayerSide.Left, _leftWordListView);
            var rightList = CreateWordList(PlayerSide.Right, _rightWordListView);
            var provider = new WordListProvider(leftList, rightList);

            Container.Bind<IWordListProvider>().To<WordListProvider>().FromInstance(provider).AsSingle();
        }

        private IWordListPresenter CreateWordList(PlayerSide side, WordListView view)
        {
            var id = side.ToString();

            Container.Bind<IWordListModel>().To<WordListModel>().AsCached().When(c => c.ParentContext.Identifier.ToString() == id);
            Container.Bind<IWordListView>().FromInstance(view).AsCached().When(c => c.ParentContext.Identifier.ToString() == id);
            Container.Bind(typeof(IInitializable), typeof(IDisposable), typeof(IWordListPresenter)).WithId(id).To<WordListPresenter>().AsCached();

            return Container.ResolveId<IWordListPresenter>(id);
        }
    }
}
