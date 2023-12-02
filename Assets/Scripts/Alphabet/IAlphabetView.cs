using System.Collections.Generic;

public interface IAlphabetView
{
    void Init(IAlphabetPresenter presenter);
    void UpdateView(IReadOnlyList<char> chars);
}