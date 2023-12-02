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
            _text.text = data.Message.Text;
            _leftBtnText.text = data.LeftButton.Text;
            _rightBtnText.text = data.RightButton.Text;

            _leftBtn.gameObject.SetActive(data.LeftButton.Active);
            _rightBtn.gameObject.SetActive(data.RightButton.Active);
            _gameObject.SetActive(data.Message.Active);
        }

        void Start()
        {
            _leftBtn.onClick.AddListener(() => _presenter.LeftBtnClick());
            _rightBtn.onClick.AddListener(() => _presenter.RightBtnClick());
        }
    }
}
