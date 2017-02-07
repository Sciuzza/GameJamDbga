using UnityEngine;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerClock : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerExitHandler
{

    public UnityEvent ButtonClicked;

    public Image ClockZone;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        this.ClockZone.gameObject.SetActive(true);
        Debug.Log("Ciao");
    }

    public void OnSelect(BaseEventData eventData)
    {
      this.ClockZone.gameObject.SetActive(true);
        Debug.Log("Ciao");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.ClockZone.gameObject.SetActive(false);
        Debug.Log("Ciao");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.ClockZone.gameObject.SetActive(false);
        Debug.Log("Ciao");
    }
}
