using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balda
{
    public class MessageView : MonoBehaviour, IMessageView
    {
        [SerializeField]
        private GameObject _gameObject;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Button _OkBtn;

        [SerializeField]
        private Button _resetBtn;

        private IMessagePresenter _presenter;

        public void Init(IMessagePresenter presenter)
        {
            _presenter = presenter;
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }

        public void Show(string text)
        {
            _text.text = text;
            _gameObject.SetActive(true);
        }

        void Start()
        {
            _OkBtn.onClick.AddListener(() => _presenter.OkClick());
            _resetBtn.onClick.AddListener(() => _presenter.ResetClick());
        }
    }
}
