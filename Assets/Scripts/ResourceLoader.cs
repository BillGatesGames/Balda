using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{

    public class ResourceLoader : MonoBehaviour
    {
        private static ResourceLoader _instance;
        public static ResourceLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject(nameof(ResourceLoader));
                    _instance = go.AddComponent<ResourceLoader>();
                }

                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }

        public T LoadLocal<T>(string fileName) where T : class
        {
            var asset = Resources.Load(fileName) as TextAsset;

            if (asset == null)
            {
                return null;
            }

            try
            {
                return JsonConvert.DeserializeObject<T>(asset.text);
            }
            catch (Exception e)
            {
                Debug.LogError($"Deserialize error: {e.Message}");
            }

            return null;
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {

        }
    }
}
