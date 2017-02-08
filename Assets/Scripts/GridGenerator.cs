using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour
{

    public GameObject go;
    public int colonne;
    public int righe;
    private SpriteRenderer[] sr;

    void Awake()
    {
        GameObject tmpGo;
        for (int i = 0; i < righe; i++) //4
        {
            for (int j = 0; j < colonne; j++)   //3
            {
                sr = GetComponents<SpriteRenderer>();
                tmpGo = Instantiate(go);
                tmpGo.transform.position = new Vector2(i, j);
                tmpGo.name = "room " + i + " " + j;
            }
        }
    }
}
