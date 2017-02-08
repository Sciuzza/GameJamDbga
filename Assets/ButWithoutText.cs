using UnityEngine;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.EventSystems;

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

        this.ButtonClicked.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
