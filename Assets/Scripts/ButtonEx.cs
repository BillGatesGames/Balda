using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Balda
{
    [System.Serializable]
    public class ButtonState
    {
        public Sprite BackSprite;
        public Color BackColor;
        public Color TextColor;
    }

    public class ButtonEx : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public Action<ButtonEx> OnClick;

        [SerializeField]
        private Image _back;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private bool _changeSizeOnClick = true;

        [SerializeField]
        private float _sizeMultiplier = 0.95f;

        [Header("States")]
        [SerializeField]
        private ButtonState _normalState;

        [SerializeField]
        private ButtonState _selectedState;

        [SerializeField]
        private ButtonState _disabledState;

        private bool _interactable;
        public bool Interactable
        {
            get
            {
                return _interactable;
            }

            set
            {
                _interactable = value;

                if (_interactable)
                {
                    SetState(_normalState);
                }
                else
                {
                    SetState(_disabledState);
                }
            }
        }

        private bool _selected;
        public bool Selected
        {
            get
            {
                return _selected;
            }

            set
            {
                if (_interactable)
                {
                    _selected = value;

                    if (_selected)
                    {
                        SetState(_selectedState);
                    }
                    else
                    {
                        SetState(_normalState);
                    }
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable)
            {
                return;
            }

            OnClick?.Invoke(this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable)
            {
                return;
            }

            if (_changeSizeOnClick)
            {
                transform.localScale *= _sizeMultiplier;
            }   
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        private void SetState(ButtonState state)
        {
            if (_back != null)
            {
                _back.sprite = state.BackSprite;
                _back.color = state.BackColor;
            }

            if (_text != null)
            {
                _text.color = state.TextColor;
            }        
        }

        void Awake()
        {
            Interactable = true;
        }

        void Start()
        {

        }
    }
}
