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
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameCont>().PlaySound(1, 3);
            this.ButtonClicked.Invoke();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }
}
