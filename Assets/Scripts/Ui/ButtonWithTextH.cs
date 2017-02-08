using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonWithTextH : MonoBehaviour, IPointerEnterHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler, IPointerExitHandler
{

    public UnityEvent ButtonClicked;

    private int originalFs;


    public void OnPointerEnter(PointerEventData eventData)
    { 
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        this.GetComponentInChildren<Text>().fontSize = (int)(this.originalFs * 1.5f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = (int)(this.originalFs * 1.5f);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = this.originalFs;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        this.ButtonClicked.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = this.originalFs;
    }

    private void Awake()
    {
        this.originalFs = this.GetComponentInChildren<Text>().fontSize;
    }
}
