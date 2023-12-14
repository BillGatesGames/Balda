using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Balda
{
    public sealed class SceneLoader : MonoBehaviour, ISceneLoadHandler
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Load(int index)
        {
            SceneManager.LoadScene(index);
        }

        void Start()
        {
            _signalBus.Subscribe<int>(Load);
        }

        void OnDestroy()
        {
            _signalBus.Unsubscribe<int>(Load);
        }
    }
}
