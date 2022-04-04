using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WolfUISystem.Presets
{
    public class PopupScreen : WolfUISystem.ScreenBase
    {
        public enum PopupType
        {
            ButtonGroup
        }

        [SerializeField]
        List<PopupScreenPanel> popups;

        public void InitializePopup(PopupType popupType, object arg)
        {
            foreach (PopupScreenPanel panel in popups)
            {
                if (panel.popupType == popupType)
                {
                    panel.Initialize (arg);
                }
                else
                {
                    panel.Hide();
                }
            }
        }

        public override void Initialize()
        {
            foreach (PopupScreenPanel panel in popups)
            {
                panel.Hide();
            }
        }
    }
}
