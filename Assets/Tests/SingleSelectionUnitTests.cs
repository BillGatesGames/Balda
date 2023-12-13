using Zenject;
using NUnit.Framework;
using System.Linq;

namespace Balda.Tests
{
    [TestFixture]
    public class SingleSelectionUnitTests : ZenjectUnitTestFixture
    {
        private IFieldModel _model;

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
            _model.Selection.Mode = SelectionMode.Single;
        }

        /// <summary>
        /// ��������� ������ ������ ��� ������� � �������
        /// </summary>
        [Test]
        public void SelectEmptyCellWithoutOccupiedNeighbors()
        {
            _model.Selection.Clear();

            var cells = _model.GetField().GetEmptyCellsWithoutOccupiedNeighbors();

            foreach (var cell in cells)
            {
                _model.Selection.Select(cell);

                Assert.AreEqual(0, _model.Selection.Positions.Count);
            }
        }

        /// <summary>
        /// ��������� ������ ������, � ������� ������ � �������
        /// </summary>
        [Test]
        public void SelectEmptyCellAroundOccupiedCells()
        {
            _model.Selection.Clear();

            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells();

            foreach (var cell in cells)
            {
                _model.Selection.Select(cell);

                Assert.AreEqual(1, _model.Selection.Positions.Count);
            }
        }

        /// <summary>
        /// ������� ��������� ������ ������, � ������� ������ � �������,
        /// � ����� ������ ������ � ������� ��������
        /// </summary>
        [Test]
        public void SelectEmptyCellAroundOccupiedCellsThenEmptyCell()
        {
            _model.Selection.Clear();

            var cells1 = _model.GetField().GetEmptyCellsAroundOccupiedCells();
            var cells2 = _model.GetField().GetEmptyCellsWithoutOccupiedNeighbors();

            foreach (var cell1 in cells1)
            {
                foreach (var cell2 in cells2)
                {
                    _model.Selection.Select(cell1);
                    _model.Selection.Select(cell2);

                    Assert.AreEqual(0, _model.Selection.Positions.Count);
                }
            }
        }

        /// <summary>
        /// ������� ��������� ������ ������, � ������� ������ � �������,
        /// � ����� ������ � ������
        /// </summary>
        [Test]
        public void SelectEmptyCellAroundOccupiedCellsThenOccupiedCell()
        {
            _model.Selection.Clear();

            var cells1 = _model.GetField().GetEmptyCellsAroundOccupiedCells();
            var cells2 = _model.GetField().GetOccupiedCells();

            foreach (var cell1 in cells1)
            {
                foreach (var cell2 in cells2)
                {
                    _model.Selection.Select(cell1);
                    _model.Selection.Select(cell2);

                    Assert.AreEqual(0, _model.Selection.Positions.Count);
                }
            }
        }

        /// <summary>
        /// ��� ���� ������� ���� � �� �� ������ ������, � ������� ������ � ������� 
        /// </summary>
        [Test]
        public void DoubleSelectEmptyCellAroundOccupiedCells()
        {
            _model.Selection.Clear();

            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells();

            foreach (var cell in cells)
            {
                _model.Selection.Select(cell);
                _model.Selection.Select(cell);

                Assert.AreEqual(1, _model.Selection.Positions.Count);
            }
        }

        /// <summary>
        /// ���������� ����� � ������, � ����� ������� ��
        /// </summary>
        [Test]
        public void SetLettersAndSelect()
        {
            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells();

            foreach (var cell in cells)
            {
                _model.Selection.Clear();
                _model.SetChar(cell, 'X');
                _model.Selection.Select(cell);

                Assert.AreEqual(1, _model.Selection.Positions.Count);

                _model.DeleteLastChar();
            }
        }

        /// <summary>
        /// ���������� ����� � ������ � ��� ���� ������� ��
        /// </summary>
        /// <remarks>
        /// � ���������� ������ ������ �������� ���������
        /// </remarks>
        [Test]
        public void DoubleSetLettersAndSelect()
        {
            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells();

            foreach (var cell in cells)
            {
                _model.Selection.Clear();
                _model.SetChar(cell, 'X');
                _model.Selection.Select(cell);
                _model.Selection.Select(cell);

                Assert.AreEqual(1, _model.Selection.Positions.Count);

                _model.DeleteLastChar();
            }
        }

        /// <summary>
        /// ���������� � ������� �����, � ����� ������� ������ ������,
        /// � ������� ������ � �������
        /// </summary>
        /// <remarks>
        /// � ���������� ������ ���� ������� ������ ������
        /// </remarks>
        [Test]
        public void SetLetterAndSelectThenSelectEmptyCellAroundOccupiedCells()
        {
            var cells = _model.GetField().GetEmptyCellsAroundOccupiedCells().ToList();

            for (int i = 0; i < cells.Count; i++)
            {
                for (int j = 0; j < cells.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    _model.Selection.Clear();
                    _model.SetChar(cells[i], 'X');
                    _model.Selection.Select(cells[i]);
                    _model.Selection.Select(cells[j]);

                    Assert.AreEqual(1, _model.Selection.Positions.Count);
                    Assert.AreEqual(cells[j], _model.Selection.Positions.First());

                    _model.DeleteLastChar();
                }
            }
        }

        /// <summary>
        /// ���������� � ������� �����, � ����� ������� ������ ������,
        /// � ������� ������ ����� ������ ������
        /// </summary>
        /// <remarks>
        /// � ���������� ��������� ������ ���� ��������
        /// </remarks>
        [Test]
        public void SetLetterAndSelectThenSelectEmptyCell()
        {
            var cells1 = _model.GetField().GetEmptyCellsAroundOccupiedCells().ToList();
            var cells2 = _model.GetField().GetEmptyCellsWithoutOccupiedNeighbors().ToList();

            for (int i = 0; i < cells1.Count; i++)
            {
                for (int j = 0; j < cells2.Count; j++)
                {
                    _model.Selection.Clear();
                    _model.SetChar(cells1[i], 'X');
                    _model.Selection.Select(cells1[i]);
                    _model.Selection.Select(cells2[j]);

                    Assert.AreEqual(0, _model.Selection.Positions.Count);

                    _model.DeleteLastChar();
                }
            }
        }

        /// <summary>
        /// ���������� � ������� �����, � ����� ������� ������ � ������
        /// </summary>
        /// <remarks>
        /// � ���������� ��������� ������ ���� ��������
        /// </remarks>
        [Test]
        public void SetLetterAndSelectThenSelectOccupiedCell()
        {
            var cells1 = _model.GetField().GetEmptyCellsAroundOccupiedCells().ToList();
            var cells2 = _model.GetField().GetOccupiedCells().ToList();

            for (int i = 0; i < cells1.Count; i++)
            {
                for (int j = 0; j < cells2.Count; j++)
                {
                    _model.Selection.Clear();
                    _model.SetChar(cells1[i], 'X');
                    _model.Selection.Select(cells1[i]);
                    _model.Selection.Select(cells2[j]);

                    Assert.AreEqual(0, _model.Selection.Positions.Count);

                    _model.DeleteLastChar();
                }
            }
        }

        public override void Teardown()
        {
            base.Teardown();

            _model = null;
        }
    }
}