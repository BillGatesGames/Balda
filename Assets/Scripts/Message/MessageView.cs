using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Balda
{
    public class MessageView : MonoBehaviour, IMessageView
    {
        [SerializeField]
        private GameObject _gameObject;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TextMeshProUGUI _title;

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

        [Inject]
        private ILocalizationManager _localizationManager;

        private IMessagePresenter _presenter;

        public void Init(IMessagePresenter presenter)
        {
            _presenter = presenter;

            Subscribe();
        }

        public void Hide()
        {
            SetActive(false);
        }

        public void SetData(MessageData data)
        {
            if (_title != null)
            {
                _title.text = GetLocal(data.Title.Alias);
                _title.gameObject.SetActive(data.Title.Active);
            }

            _text.text = GetLocal(data.Message.Alias);
            _leftBtnText.text = GetLocal(data.LeftButton.Alias);
            _rightBtnText.text = GetLocal(data.RightButton.Alias);

            _leftBtn.gameObject.SetActive(data.LeftButton.Active);
            _rightBtn.gameObject.SetActive(data.RightButton.Active);

            SetActive(data.Message.Active);
        }

        private string GetLocal(string text)
        {
            return _localizationManager.Get(text);
        }

        private void SetActive(bool value)
        {
            _canvasGroup.alpha = value ? 1f : 0f;
            _canvasGroup.blocksRaycasts = value;
            _canvasGroup.interactable = value;
        }

        private void Subscribe()
        {
            Unsubscribe();

            _leftBtn.onClick.AddListener(_presenter.LeftBtnClick);
            _rightBtn.onClick.AddListener(_presenter.RightBtnClick);
        }

        private void Unsubscribe()
        {
            if (_presenter != null)
            {
                _leftBtn.onClick.RemoveListener(_presenter.LeftBtnClick);
                _rightBtn.onClick.RemoveListener(_presenter.RightBtnClick);
            }
        }

        void Awake()
        {
            SetActive(false);
        }

        void Start()
        {

        }

        void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
