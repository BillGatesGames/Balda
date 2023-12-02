using System.Collections;
using System.Collections.Generic;

public interface IWordListView
{
    void Init(IWordListPresenter presenter);
    void AddWord(string word);
    void UpdateView(IEnumerable<string> words, string totalText);
}