using UnityEngine;
using System.Collections;

//un nemico
//spawna ad un orario e rimane fino ad un altro orario
//qui si bilancia il gioco
public class Enemy : MonoBehaviour
{
    public int oraInizio, oraFine, arraySpritePosition;
    public RoomManager room;

    void Start()
    {
        InitHour();

    }

    private void InitHour()
    {
        oraInizio = Random.Range(0, 12);
        oraFine = (oraInizio + Sync.NHE) % Sync.MODH;
    }

    public bool IsActive()
    {
        return Sync.actualHour % Sync.MODH >= oraInizio && Sync.actualHour % Sync.MODH <= oraFine;
    }
}
