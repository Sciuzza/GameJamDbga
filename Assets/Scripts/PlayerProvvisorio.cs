using UnityEngine;
using System.Collections;

public class PlayerProvvisorio : MonoBehaviour
{

    public void NewPosition(Vector2 pos)
    {        
        transform.position = pos+ new Vector2(0.22f,0);
    }

}
