using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balda
{
    public class SceneLoader : MonoBehaviour, ISceneLoadHandler
    {
        public void Load(int index)
        {
            SceneManager.LoadScene(index);
        }

        void Start()
        {
            EventBus.Register(this);
        }

        void OnDestroy()
        {
            EventBus.Unregister(this);
        }
    }
}
