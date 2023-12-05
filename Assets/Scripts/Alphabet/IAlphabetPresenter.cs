using System;

namespace Balda
{
    public interface IAlphabetPresenter : IStateHandler, IPresenter
    {
        event Action<Cell> OnCellClick;
        void CellClick(Cell cell);
    }
}