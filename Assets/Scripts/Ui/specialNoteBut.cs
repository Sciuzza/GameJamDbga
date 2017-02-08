using UnityEngine;
using System.Collections;

using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class event_button : UnityEvent<Button>
{
    
}

public class specialNoteBut : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, IPointerExitHandler
{

    public event_button NoteButClicked;


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
        {
            this.NoteButClicked.Invoke(this.gameObject.GetComponent<Button>());
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
