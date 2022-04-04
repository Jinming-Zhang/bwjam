using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class
ButtonToggleSpriteSwapper
: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    [SerializeField]
    Sprite selectedSprite;

    [SerializeField]
    Sprite deselectedSprite;

    Image _buttonImage;

    Image buttonImage
    {
        get
        {
            if (!_buttonImage)
            {
                _buttonImage = GetComponent<Image>();
            }
            return _buttonImage;
        }
    }

    private bool isSelected;

    public bool IsSelected
    {
        get
        {
            return isSelected;
        }
        set
        {
            isSelected = value;
            SetSprite (value);
        }
    }

    private void SetSprite(bool isSelected)
    {
        buttonImage.sprite = isSelected ? selectedSprite : deselectedSprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetSprite(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!IsSelected)
        {
            SetSprite(false);
        }
    }
}
