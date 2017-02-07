using UnityEngine;
using System.Collections;

public class GridGenerator : MonoBehaviour {
    
    public GameObject go;
    public int colonne;
    public int righe;
    private SpriteRenderer[] sr;

	void Awake () {
        
        for (int i = 0; i < righe; i++)
        {
            for (int j = 0; j < colonne; j++)
            {
                sr = GetComponents<SpriteRenderer>();
                Instantiate(go);
                go.transform.position = new Vector2(i, j);
                go.name = "Room" + i + " " + j;                                                                   
            }
        }
	}	
}
