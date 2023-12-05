using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField]
        private ButtonEx _button;

        private void Button_OnClick(ButtonEx button)
        {
            EventBus.RaiseEvent<ISceneLoadHandler>(h => h.Load(Constants.Scenes.MENU));
            EventBus.Clear();
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
