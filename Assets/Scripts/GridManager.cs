using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{

    public GameObject Enemy;
    public int X, Y, nOfEnemies;
    public Sprite[] srEnemies;
    public RoomManager[] listOfRooms;
    public int maxNumber;

    // Use this for initialization
    void Awake()
    {
        Sync.isReady = false;
        InitHourCost();
        Sync.actualHour = 0;
    }

    void Start()
    {
        InitRooms();

        //da sistemare? così parto dall'ora (1 + hourCost)
        listOfRooms[Random.Range(0, listOfRooms.Length)].ChangeRoom();

        InitEnemies();
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
        SpriteRenderer sr;
        int[] tmpEnemies = new int[nOfEnemies];
        for (int i = 0; i < nOfEnemies; i++)
        {
            tmpEnemies[i] = -1;
        }
        for (int i = 0; i < nOfEnemies; i++)
        {
            do
            {
                tmpEnemies[i] = Random.Range(0, X * Y);
            } while (!IsDifferentValue(tmpEnemies, i) || listOfRooms[tmpEnemies[i]].isActiveRoom);
            Instantiate(Enemy);
            Enemy.transform.position = listOfRooms[tmpEnemies[i]].transform.position;
            Enemy.name = "enemy " + (i + 1);
            Enemy.GetComponent<SpriteRenderer>().sprite = srEnemies[i];
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
                listOfRooms[i * X + j] = GameObject.Find("room " + i + " " + j + "(Clone)").GetComponent<RoomManager>();
            }
        }
    }
}

