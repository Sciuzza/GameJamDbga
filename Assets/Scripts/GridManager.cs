using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


public class GridManager : MonoBehaviour
{
    public int X, Y;
    public Sprite[] sEnemies;
    public Sprite[] sFloor;
    public int maxNumber;

    public PlayerProvvisorio player;
    public RoomManager[] listOfRooms;
    public Enemy[] listOfEnemies;

    void Awake()
    {
        Sync.isReady = false;
        Sync.actualHour = 0;
    }

    void Start()
    {
        InitRooms();
        InitHourCost();

        InitPlayer();

        InitEnemies();
        Sync.isReady = true;
    }


    /// <summary>
    /// inizializza randomicamente i costi (in ore) di ingresso per ogni Room.
    /// memorizzati in RoomManager
    /// </summary>
    private void InitHourCost()
    {
        int[] counts = new int[maxNumber];
        //for(int i = 0; i < listOfRooms.Length; i++)
        foreach (var item in listOfRooms)
        {
            do
            {
                item.hourCost = Random.Range(2, maxNumber + 2);
            } while (counts[item.hourCost - 2]++ >= listOfRooms.Length / maxNumber);
        }
    }

    private void InitPlayer()
    {
        int r;
        do
        {   //impedisco che sia l'exit
            r = Random.Range(0, listOfRooms.Length);
        } while (listOfRooms[r].isExitRoom);
        listOfRooms[r].isStartingRoom = listOfRooms[r].isActiveRoom = true;
        player.NewPosition(listOfRooms[r].transform.position);
    }

    private void InitEnemies()
    {
        int[] tmpEnemies = new int[listOfEnemies.Length];
        for (int i = 0; i < tmpEnemies.Length; i++)
        {
            tmpEnemies[i] = -1;
        }
        for (int i = 0; i < listOfEnemies.Length; i++)
        {
            listOfEnemies[i] = GameObject.Find("enemy " + (i + 1)).GetComponent<Enemy>();
            do
            {
                tmpEnemies[i] = Random.Range(0, X * Y);
            } while (!IsDifferentValue(tmpEnemies, i) || listOfRooms[tmpEnemies[i]].isActiveRoom);
            listOfEnemies[i].gameObject.GetComponent<SpriteRenderer>().sprite = sEnemies[i];
            listOfEnemies[i].gameObject.transform.position = listOfRooms[tmpEnemies[i]].transform.position - new Vector3(0.22f, 0);
            listOfEnemies[i].arraySpritePosition = i;
            listOfEnemies[i].room = listOfRooms[tmpEnemies[i]];
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="a">array di interi controllato</param>
    /// <param name="v">indice di a che deve essere diverso</param>
    /// <returns>true se a[v] != da tutti gli altri a</returns>
    private bool IsDifferentValue(int[] a, int v)
    {
        for (int i = 0; i < a.Length; i++)
        {
            if (i != v)
            {
                if (a[i] == a[v])
                    return false;
            }
        }
        return true;
    }

    private void InitRooms()
    {
        listOfRooms = new RoomManager[X * Y];
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                listOfRooms[i * Y + j] = GameObject.Find("room " + i + " " + j).GetComponent<RoomManager>();
            }
        }
    }

    public void NextTurn()
    {
        UpdateEnemies();
    }


    private void UpdateEnemies()
    {
        //accedo alle room contenenti un enemy
        for (int i = 0; i < listOfEnemies.Length; i++)
        {
            listOfEnemies[i].room.isEnemyRoom = listOfEnemies[i].IsActive();
            listOfEnemies[i].gameObject.GetComponent<SpriteRenderer>().enabled = listOfEnemies[i].IsActive();

        }
    }
}