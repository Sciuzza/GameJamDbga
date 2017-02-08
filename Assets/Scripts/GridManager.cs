using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{

    public GameObject Enemy;
    public RoomManager[] listOfRooms;
    public int maxNumber;

    // Use this for initialization
    void Awake()
    {
        Sync.isReady = false;
        InitHourCost();
        Sync.actualHour = 0;
        InitEnemies();
    }

    void Start()
    {
        //da sistemare? così parto dall'ora (1 + hourCost)
        listOfRooms[Random.Range(0, listOfRooms.Length)].ChangeRoom();
        Sync.isReady = true;
    }

    /// <summary>
    /// inizializza randomicamente i costi (in ore) di ingresso per ogni Room.
    /// memorizzati in RoomManager
    /// </summary>
    private void InitHourCost()
    {
        System.Random r = new System.Random();
        int[] counts = new int[maxNumber];
        //for(int i = 0; i < listOfRooms.Length; i++)
        foreach (var item in listOfRooms)
        {
            do
            {
                item.hourCost = r.Next(1, maxNumber + 1);
            } while (counts[item.hourCost - 1]++ >= listOfRooms.Length / maxNumber);
        }
    }

    private void InitEnemies()
    {
        for (int i = 0; i < 16; i++)
        {
            Instantiate(Enemy);
        }
    }
}
