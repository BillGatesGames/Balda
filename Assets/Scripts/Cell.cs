using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Balda
{
    public class Cell : MonoBehaviour
    {
        public Action<Cell> OnClick;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Button _button;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private Color _normalColor;

        [SerializeField]
        private Color _selectedColor;

        public int X;
        public int Y;

        private char? _char;

        public char? Char
        {
            set
            {
                if (value == null)
                {
                    _text.text = string.Empty;
                }
                else
                {
                    _text.text = value.Value.ToString();
                }

                _char = value;
            }

            get
            {
                return _char;
            }
        }

        public void Select(bool selected)
        {
            _image.color = selected ? _selectedColor : _normalColor;
        }

        void Start()
        {
            _button.onClick.AddListener(() => OnClick?.Invoke(this));
        }
    }
}
