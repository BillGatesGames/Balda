using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Balda
{
    public class WordListView : MonoBehaviour, IWordListView
    {
        [SerializeField]
        private RectTransform _itemsParent;

        [SerializeField]
        private TextMeshProUGUI _totalText;

        [SerializeField]
        private WordListItem _wordListItemPrefab;

        private void SetTotalText(string text)
        {
            _totalText.text = text;
        }

        private void Clear()
        {
            var items = GetComponentsInChildren<WordListItem>(true);

            foreach (var item in items)
            {
                Destroy(item.gameObject);
            }
        }

        public void UpdateView(IEnumerable<string> words, string totalText)
        {
            Clear();

            foreach (var word in words)
            {
                var item = Instantiate(_wordListItemPrefab, _itemsParent);
                item.SetWord(word);
            }

            SetTotalText(totalText);
        }

        void Start()
        {

        }

        void OnDestroy()
        {
            Clear();
        }
    }
}
