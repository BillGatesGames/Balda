using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        var go = new GameObject(typeof(T).ToString());
                        _instance = go.AddComponent<T>();
                    }
                }

                return _instance;
            }

            protected set
            {
                _instance = value;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
                return;
            }

            _instance = this as T;

            DontDestroyOnLoad(gameObject);
        }

        protected virtual void Start()
        {

        }


        protected virtual void Update()
        {

        }
    }
}
