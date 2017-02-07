using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {

    public GameObject go;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Instantiate(go);
                go.name = "room " + i + " " + j;
                go.transform.position = new Vector2(i, j);                                          
            }
        }

	}
	
}
