using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    //public int oraInizio, oraFine;
    public RoomManager room;
    public bool[] hours = new bool[Sync.MODH];
    public int arraySpritePosition;
    public bool isDead;
    void Start()
    {
        isDead = false;
    }

    public bool IsActive()
    {
        return hours[Sync.getHour()] && !isDead;
    }
}
