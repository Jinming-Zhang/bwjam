using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupButton : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Button Button;

    [SerializeField]
    TMPro.TextMeshProUGUI ButtonText;

    public void InitializeButton(
        string text,
        UnityEngine.Events.UnityAction onButtonClickedHandler
    )
    {
        ButtonText.text = text;
        Button.onClick.RemoveAllListeners();
        Button.onClick.AddListener (onButtonClickedHandler);
    }
}
