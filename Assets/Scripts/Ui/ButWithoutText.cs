using UnityEngine;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButWithoutText : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, IPointerExitHandler
{

    public UnityEvent ButtonClicked;


    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<Button>().IsInteractable())
        this.ButtonClicked.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
