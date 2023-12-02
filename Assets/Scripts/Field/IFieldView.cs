using System.Collections.Generic;
using UnityEngine;

public interface IFieldView
{
    void Init(IFieldPresenter presenter);
    void UpdateView(char?[,] field);
    void UpdateSelection(IReadOnlyCollection<Vector2Int> selection);
    void ClearSelection();
    Cell Get(Vector2Int pos);
}