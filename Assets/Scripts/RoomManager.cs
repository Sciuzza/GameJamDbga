﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{

    public bool isStartingRoom;
    public bool isActiveRoom;
    public bool isExitRoom;
    public bool isEnemyZone;
    public int hourCost;
    public Color spriteColor = Color.white;
    public float fadeInTime = 1.5f;
    public float fadeOutTime = 3f;
    public float delayToFadeOut = 5f;
    public float delayToFadeIn = 5f;

    public List<GameObject> nearRoom;

    private PlayerProvvisorio player;
    private SpriteRenderer sprite;

    private int old_hourCost;


    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerProvvisorio>();
    }
    void Start()
    {
        old_hourCost = hourCost;
        EnemySetHourCost();
        InitNearRoom();
    }


    void Update()
    {
        if (this.isStartingRoom || this.isActiveRoom)
        {
            StartCoroutine("FadeCycle");
        }

    }
    void OnMouseDown()
    {
        StartCoroutine("FadeCycle");
        if (Sync.isReady && !isActiveRoom && IsNearPlayer())
        {
            Sync.isReady = false;
            ChangeRoom();
            Sync.isReady = true;
        }
    }

    //da chiamare ogni ora
    /// <summary>
    /// imposta il costo per entrare in questa Room in base a isEnemyZone
    /// </summary>
    private void EnemySetHourCost()
    {
        if (isEnemyZone)
        {
            hourCost = 0;
        }
        else
            hourCost = old_hourCost;
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
            name[2] = Convert.ToString(Convert.ToInt32(name[2].Substring(0, name[2].IndexOf('('))) + i);
            nearRoom.Add(GameObject.Find(name[0] + ' ' + name[1] + ' ' + name[2] + "(Clone)"));
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
        Sync.actualHour += hourCost;
        player.NewPosition(transform.position);
        Debug.Log("hour: " + ((Sync.actualHour % Sync.MODH) + 1));
    }

    IEnumerator FadeCycle()
    {
        float fade = 0f;
        float startTime;
        while (true)
        {
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
            StopCoroutine("FadeCycle");
        }
    }
}

