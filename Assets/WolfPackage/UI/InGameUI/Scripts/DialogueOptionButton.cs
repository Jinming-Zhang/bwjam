using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueSystem;
using System;
using UnityEngine.UI;

public class DialogueOptionButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] TMPro.TextMeshProUGUI buttonText;
    Action<DialogueOption> clickedCB;
    DialogueOption option;
    public void Initialize(DialogueOption option, Action<DialogueOption> onOptionSelected)
    {
        buttonText.text = option.optionText;
        button.onClick.RemoveAllListeners();
        clickedCB = onOptionSelected;
        this.option = option;
        //button.onClick.AddListener(() => onOptionSelected.Invoke(option));
    }

    public void OnButtonClicked()
    {
        clickedCB?.Invoke(option);
    }
}
