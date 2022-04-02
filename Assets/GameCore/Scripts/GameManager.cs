using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance;

        public static GameManager Instance => instance;

        public GameResourceLocator ResourceLocator;
        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy (gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
