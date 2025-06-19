using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonHoverEmitter : MonoBehaviour, IPointerEnterHandler
{
    public PauseMenuUI menuUI;

    public void OnPointerEnter(PointerEventData eventData)
    {
       if (menuUI != null)
        {
            menuUI.InvokeOnButtonHovered();
        }
    }
}