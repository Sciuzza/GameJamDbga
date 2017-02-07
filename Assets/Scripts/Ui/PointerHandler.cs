using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointerHandler : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, IPointerExitHandler
{

    public UnityEvent ButtonClicked;

    public void OnPointerEnter(PointerEventData eventData)
    { 
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        this.GetComponentInChildren<Text>().fontSize = 140;
    }

    public void OnSelect(BaseEventData eventData)
    {
       // GameObject.FindGameObjectWithTag("GameController").GetComponent<SoundManager>().PlaySound(1, 1);
        this.GetComponentInChildren<Text>().fontSize = 140;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = 100;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        this.ButtonClicked.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = 100;
    }
}
