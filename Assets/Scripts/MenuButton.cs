using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Balda
{
    public sealed class MenuButton : MonoBehaviour
    {
        [SerializeField]
        private ButtonEx _button;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Button_OnClick(ButtonEx button)
        {
            _signalBus.Fire(Constants.Scenes.MENU);
        }

        void Start()
        {
            _button.OnClick = Button_OnClick;
        }

        void Update()
        {

        }

        void OnDestroy()
        {
            _button.OnClick = null;
        }
    }
}
