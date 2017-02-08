using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public GameCont gc;
    public bool isStartingRoom, isExitRoom;
    public bool isActiveRoom, isEnemyRoom, isUncovered;
    public int hourCost;
    public Color spriteColor = Color.white;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 3f;
    public float delayToFadeOut = 5f;
    public float delayToFadeIn = 5f;

    public List<GameObject> nearRoom;

    private GridManager refGM;
    private SpriteRenderer sprite;



    void Awake()
    {
        gc = FindObjectOfType<GameCont>();
        nearRoom.Clear();
        refGM = GameObject.Find("[GridManager]").GetComponent<GridManager>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        InitNearRoom();
        if (this.isExitRoom || this.isStartingRoom)
        {
            StartCoroutine("FadeCycle");
        }
    }

    void OnMouseDown()
    {

        if (Sync.isReady && !isActiveRoom && IsNearPlayer())
        {
            Sync.isReady = false;
            gc.DisablingNote();
            ChangeRoom();
            refGM.NextTurn();
            if (!isStartingRoom && !isExitRoom && !isUncovered)
                StartCoroutine("FadeCycle");

            else            
                Sync.isReady = true;
            
            isUncovered = true;
            //Sync.isReady = true;
        }
    }

    /// <summary>
    /// inizializza la lista nearRoom con i GameOgject che sono adiacenti a questa Room.
    /// sx, giù, dx, su
    /// </summary>
    private void InitNearRoom()
    {
        string[] name;
        for (int i = -1; i < 2; i += 2)
        {
            name = gameObject.name.Split(' ');
            name[1] = Convert.ToString(Convert.ToInt32(name[1]) + i);
            nearRoom.Add(GameObject.Find(name[0] + ' ' + name[1] + ' ' + name[2]));
            name = gameObject.name.Split(' ');
            name[2] = Convert.ToString(Convert.ToInt32(name[2]) + i);
            nearRoom.Add(GameObject.Find(name[0] + ' ' + name[1] + ' ' + name[2]));
        }
    }

    /// <summary>
    /// verifica se il player è in una stanza vicina.
    /// Utilizza i RoomManager
    /// </summary>
    private bool IsNearPlayer()
    {
        foreach (var item in nearRoom)
        {
            if (item != null && item.GetComponent<RoomManager>().isActiveRoom)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// sposto il player
    /// </summary>
    public void ChangeRoom()
    {
        foreach (var item in nearRoom)
        {
            if (item != null)
            {
                item.GetComponent<RoomManager>().isActiveRoom = false;
            }
        }
        isActiveRoom = true;
        gc.CalculatingNewNotes(this.hourCost);
        Sync.actualHour += hourCost;
        if (this.isExitRoom)
        {
           gc.ActiveExitPanel();
        }
        

        refGM.player.NewPosition(transform.position);

        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = refGM.sFloor[hourCost - 2];
        Debug.Log("hour: " + ((Sync.getHour()) + 1));
    }

    IEnumerator FadeCycle()
    {
        float fade = 0f;
        float startTime;
        startTime = Time.time;
        while (fade < 1f)
        {
            fade = Mathf.Lerp(0f, 1f, (Time.time - startTime) / fadeInTime);
            spriteColor.a = fade;
            sprite.color = spriteColor;
            yield return null;
            //print("Step 1");
        }

        fade = 1f;
        spriteColor.a = fade;
        sprite.color = spriteColor;
        yield return new WaitForSeconds(delayToFadeOut);
        //print("Step 2");

        startTime = Time.time;
        while (fade > 0f)
        {
            fade = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeOutTime);
            spriteColor.a = fade;
            sprite.color = spriteColor;
            yield return null;
            //print("Step 3");
        }
        fade = 0f;
        spriteColor.a = fade;
        sprite.color = spriteColor;
        yield return new WaitForSeconds(delayToFadeIn);
        sprite.enabled = false;
        Sync.isReady = true;
        gc.EnablingNote();
        
    }
}

