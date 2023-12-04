using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{

    public class ResourceLoader : MonoBehaviour
    {
        public static ResourceLoader Instance { get; private set; }

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
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {

        }
    }
}
