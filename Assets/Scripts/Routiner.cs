using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Balda
{
    public class Routiner : MonoBehaviour
    {
        public static Routiner Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {

        }

        void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}
