using System;
using Zenject;

namespace Balda
{
    public interface IAlphabetPresenter : IStateHandler, IPresenter
    {
        event Action<Cell> OnCellClick;
        void CellClick(Cell cell);
    }
}