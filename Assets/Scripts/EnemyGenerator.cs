using UnityEngine;
using System.Collections;

public class EnemyGenerator : MonoBehaviour
{

    public GameObject go;
    public int n;
    void Awake()
    {
        GameObject tmpGo;
        for (int i = 0; i < n; i++)
        {
            tmpGo = Instantiate(go);
            tmpGo.name = "enemy " + (i + 1);
        }
    }
}
