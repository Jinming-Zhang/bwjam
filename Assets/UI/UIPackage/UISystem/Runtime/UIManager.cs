using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace WolfUISystem
{
    internal class FlexibleScreenStack
    {
        List<ScreenBase> screens;

        public int Count => screens.Count;

        public bool IsReadOnly => false;

        public FlexibleScreenStack()
        {
            screens = new List<ScreenBase>();
        }

        public void Push(ScreenBase screen)
        {
            screens.Add (screen);
        }

        public void PushUnderTop(ScreenBase screen)
        {
            if (screens.Count == 0)
            {
                screens.Add (screen);
            }
            else
            {
                screens.Insert(screens.Count - 1, screen);
            }
        }

        public ScreenBase Pop()
        {
            if (screens.Count > 0)
            {
                ScreenBase last = screens[screens.Count - 1];
                screens.RemoveAt(screens.Count - 1);
                return last;
            }
            else
            {
                return null;
            }
        }

        public ScreenBase Peek()
        {
            if (screens.Count > 0)
            {
                return screens[screens.Count - 1];
            }
            else
            {
                return null;
            }
        }

        public void Clear()
        {
            screens.Clear();
        }

        public bool Contains(ScreenBase item)
        {
            return screens.Contains(item);
        }

        public bool Remove(ScreenBase item)
        {
            return screens.Remove(item);
        }
    }

    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;

        public static UIManager Instance => instance;
        [SerializeField]
        private ScreenBase[] screens;

        FlexibleScreenStack currentActiveScreens = new FlexibleScreenStack();

        private void Awake()
        {
            if (instance && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(this);
                InitializeScreens();
            }
        }

        private void InitializeScreens()
        {
            screens = GetComponentsInChildren<ScreenBase>(true);
            foreach (ScreenBase screen in screens)
            {
                screen.gameObject.SetActive(false);
                screen.Initialize();
            }
        }

#region Screen Stack Functions
        /// <summary>
        /// Pop the top most screen in the stack
        /// </summary>
        /// <returns></returns>
        public ScreenBase PopScreen()
        {
            if (currentActiveScreens.Count > 0)
            {
                ScreenBase popScreen = currentActiveScreens.Pop();
                popScreen.OnScreenPoppedOut();
                popScreen.SortingOrder = 0;
                return popScreen;
            }
            return null;
        }

        public void PushScreen(ScreenBase screenToPush)
        {
            currentActiveScreens.Push (screenToPush);
            screenToPush.OnScreenPushedIn();
            screenToPush.SortingOrder = currentActiveScreens.Count;
        }

        public T PushScreen<T>()
            where T : ScreenBase
        {
            ScreenBase screen = GetScreenComponent<T>();
            currentActiveScreens.Push (screen);
            screen.OnScreenPushedIn();
            screen.SortingOrder = currentActiveScreens.Count;
            return screen as T;
        }

        public void PushScreenUnderTop(ScreenBase screenToPush)
        {
            currentActiveScreens.PushUnderTop (screenToPush);
            screenToPush.SortingOrder = currentActiveScreens.Count - 1;
            currentActiveScreens.Peek().SortingOrder =
                currentActiveScreens.Count;
        }

        public T PushScreenUnderTop<T>()
            where T : ScreenBase
        {
            ScreenBase screen = GetScreenComponent<T>();
            if (screen)
            {
                currentActiveScreens.PushUnderTop (screen);
                screen.SortingOrder = currentActiveScreens.Count - 1;
                currentActiveScreens.Peek().SortingOrder =
                    currentActiveScreens.Count;
                return screen as T;
            }
            else
            {
                Debug.LogError($"Couldn't find screen {typeof (T)}");
                return null;
            }
        }

        public ScreenBase GetCurrentScreen()
        {
            return currentActiveScreens.Peek();
        }

        public bool TryRemoveScreen<T>()
            where T : ScreenBase
        {
            ScreenBase s = GetScreenComponent<T>();
            return currentActiveScreens.Remove(s);
        }


#endregion

        public T PopAllAndSwitchToScreen<T>()
            where T : ScreenBase
        {
            PopAllScreens();

            foreach (ScreenBase screen in screens)
            {
                if (screen is T)
                {
                    currentActiveScreens.Push (screen);
                    screen.OnScreenPushedIn();
                    screen.SortingOrder = currentActiveScreens.Count;
                    return screen as T;
                }
            }
            return null;
        }

        public T GetScreenComponent<T>()
            where T : ScreenBase
        {
            foreach (ScreenBase screen in screens)
            {
                if (screen.GetType() == typeof (T))
                {
                    return screen.GetComponent<T>();
                }
            }
            return null;
        }

        public void PopAllScreens()
        {
            while (currentActiveScreens.Count > 0)
            {
                currentActiveScreens.Pop().OnScreenPoppedOut();
            }
        }
    }
}
