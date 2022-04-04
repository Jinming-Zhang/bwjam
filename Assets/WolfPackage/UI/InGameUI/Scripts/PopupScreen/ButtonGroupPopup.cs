using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace WolfUISystem.Presets
{
    public struct ButtonGroupOption
    {
        public string ButtonText;

        public UnityAction OnButtonClickedHandler;
    }

    public class ButtonGroupPopup : PopupScreenPanel
    {
        [SerializeField]
        Transform content;

        [SerializeField]
        PopupButton buttonTemplate;

        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Initialize(object arg)
        {
            List<ButtonGroupOption> options = arg as List<ButtonGroupOption>;
            if (options == null)
            {
                Debug
                    .LogError("Wrong Argument for initializing ButtonGroupPopup");
                return;
            }
            content.ClearChildren();
            foreach (ButtonGroupOption option in options)
            {
                PopupButton b = Instantiate<PopupButton>(buttonTemplate);
                b
                    .InitializeButton(option.ButtonText,
                    option.OnButtonClickedHandler);
            }
        }
    }
}
