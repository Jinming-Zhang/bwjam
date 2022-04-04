using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfUISystem.Presets
{
    public abstract class PopupScreenPanel : MonoBehaviour
    {
        public PopupScreen.PopupType popupType;

        public abstract void Initialize(object arg);

        public abstract void Hide();
    }
}
