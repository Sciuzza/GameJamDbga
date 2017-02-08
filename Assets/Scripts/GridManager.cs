using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

//public class Event_lista_nemici : UnityEvent<Enemy[]>
//{

//}

public class GridManager : MonoBehaviour
{

    //public Event_lista_nemici listaNemici; 

    public GameObject Enemy;
    public int X, Y, nOfEnemies;
    public Sprite[] sEnemies;
    public Sprite[] sFloor;
    public int maxNumber;

    public RoomManager[] listOfRooms;
    public Enemy[] listOfEnemies;

    
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

        foreach (var item in listOfRooms)
        {
            if (item.isActiveRoom)
            {

            }
        }
        //può essere anche la stanza iniziale
        //listOfRooms[Random.Range(0, listOfRooms.Length)].isExitRoom = true;

        InitEnemies();
        Sync.isReady = true;
        //listaNemici.Invoke(listOfEnemies);


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
        GameObject go;
        listOfEnemies = new Enemy[nOfEnemies];
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
            go = Instantiate(Enemy);
            go.GetComponent<SpriteRenderer>().sprite = sEnemies[i];
            go.transform.position = listOfRooms[tmpEnemies[i]].transform.position - new Vector3(0.22f,0);
            go.name = "enemy " + (i + 1);
            listOfEnemies[i] = go.GetComponent<Enemy>();
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
                listOfRooms[i * X + j] = GameObject.Find("room " + i + " " + j + "(Clone)").GetComponent<RoomManager>();
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
        for (int i = 0; i < nOfEnemies; i++)
        {
            listOfEnemies[i].room.isEnemyRoom = listOfEnemies[i].IsActive();
            listOfEnemies[i].gameObject.GetComponent<SpriteRenderer>().enabled = listOfEnemies[i].IsActive();

        }
    }
}