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
        private Button _leftBtn;

        [SerializeField]
        private Button _rightBtn;

        [SerializeField]
        private TextMeshProUGUI _leftBtnText;

        [SerializeField]
        private TextMeshProUGUI _rightBtnText;

        private IMessagePresenter _presenter;

        public void Init(IMessagePresenter presenter)
        {
            _presenter = presenter;
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }

        public void SetData(MessageData data)
        {
            _text.text = GetLocal(data.Message.Alias);
            _leftBtnText.text = GetLocal(data.LeftButton.Alias);
            _rightBtnText.text = GetLocal(data.RightButton.Alias);

            _leftBtn.gameObject.SetActive(data.LeftButton.Active);
            _rightBtn.gameObject.SetActive(data.RightButton.Active);
            _gameObject.SetActive(data.Message.Active);
        }

        private string GetLocal(string text)
        {
            return LocalizationManager.Instance.Get(text);
        }

        void Start()
        {
            _leftBtn.onClick.AddListener(() => _presenter.LeftBtnClick());
            _rightBtn.onClick.AddListener(() => _presenter.RightBtnClick());
        }
    }
}
