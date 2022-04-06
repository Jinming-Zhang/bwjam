using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfUISystem
{
    [RequireComponent(typeof(Canvas))]
    public abstract class ScreenBase : MonoBehaviour
    {
        protected Canvas canvas;
        protected Canvas ScreenCanvas
        {
            get
            {
                if(!canvas)
                {
                    canvas = GetComponent<Canvas>();
                }
                return canvas;
            }
        }

        public int SortingOrder
        {
            get => ScreenCanvas.sortingOrder; set => ScreenCanvas.sortingOrder = value;
        }

        public abstract void Initialize();

        public virtual void OnScreenPoppedOut()
        {
            gameObject.SetActive(false);
        }

        public virtual void OnScreenPushedIn()
        {
            gameObject.SetActive(true);
        }
        public bool IsCurrentActiveScreen;
    }
}