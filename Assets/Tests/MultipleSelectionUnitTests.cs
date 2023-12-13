using Zenject;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Balda.Tests
{
    [TestFixture]
    public class MultipleSelectionUnitTests : ZenjectUnitTestFixture
    {
        private IFieldModel _model;
        private Vector2Int _letterPos;

        [SetUp]
        public void CommonInstall()
        {
            Container.BindInterfacesAndSelfTo<ResourceLoader>().AsSingle().NonLazy();
            Container.BindInstance(Constants.Localization.EN);
            Container.BindInterfacesAndSelfTo<LocalizationManager>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<Trie>().AsSingle();
            Container.BindInstance(Constants.Field.SIZE_5x5);
            Container.BindInterfacesAndSelfTo<FieldModel>().AsSingle();

            Container.Resolve<LocalizationManager>().Initialize();
            Container.Resolve<Trie>().Initialize();

            _model = Container.Resolve<FieldModel>();
            _model.Initialize();
            _model.Selection.Mode = SelectionMode.Multiple;

            _letterPos = new Vector2Int(0, 1);
        }

        private void SetLetterAndSelectWord()
        {
            _model.Selection.Clear();
            _model.SetChar(_letterPos, 'X');
            _model.Selection.Select(_letterPos);
            _model.Selection.Select(new Vector2Int(0, 2));
            _model.Selection.Select(new Vector2Int(1, 2));
        }

        [Test]
        public void SelectWord()
        {
            SetLetterAndSelectWord();

            Assert.AreEqual(3, _model.Selection.Positions.Count);
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать букву, которая не стоит
        /// рядом с какой-либо буквой выбранного слова
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectLetterThatIsNotLetterOfThisWord()
        {
            SetLetterAndSelectWord();

            _model.Selection.Select(new Vector2Int(4, 2));

            Assert.AreEqual(0, _model.Selection.Positions.Count);
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать букву в этом слове
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectLetterOfWord()
        {
            SetLetterAndSelectWord();

            _model.Selection.Select(new Vector2Int(0, 1));

            Assert.AreEqual(0, _model.Selection.Positions.Count);
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать пустую клетку, у которой 
        /// соседи с буквой
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectEmptyCellAroundOccupiedCells()
        {
            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells();

            foreach (var cell in cells)
            {
                SetLetterAndSelectWord();

                _model.Selection.Select(cell);

                Assert.AreEqual(0, _model.Selection.Positions.Count);
            }
        }

        /// <summary>
        /// Выбрать слово, а потом выбрать пустую клетку, у которой 
        /// соседи с буквой
        /// </summary>
        /// <remarks>
        /// В результате выделение должно быть сброшено
        /// </remarks>
        [Test]
        public void SelectWordThenSelectEmptyCell()
        {
            var cells = _model.GetField().GetEmptyCells();

            foreach (var cell in cells)
            {
                SetLetterAndSelectWord();

                _model.Selection.Select(cell);

                Assert.AreEqual(0, _model.Selection.Positions.Count);
            }
        }
    }
}