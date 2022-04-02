using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class GameResourceLocator : MonoBehaviour
    {
        private static GameResourceLocator instance;

        public static GameResourceLocator Instance => instance;

        public BGMTrackManager bgmTrackManager;

        public MainCamCtrl mainCamControl;

        private void Awake()
        {
            if (instance && instance != null)
            {
                Destroy (gameObject);
            }
            else
            {
                instance = this;
            }
        }
    }
}
