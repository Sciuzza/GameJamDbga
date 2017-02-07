using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

    public bool[] isDoorActive;
    public bool isStartingRoom;
    public bool isActiveRoom;
    private SpriteRenderer sr;

	// Use this for initialization
	void Start ()
    {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (this.isStartingRoom || this.isActiveRoom)
        {
            this.sr.color = Color.blue;
        }
        else
        {
            this.sr.color = Color.gray;
        }

    }
    void OnMouseDown()
    {
        this.isActiveRoom = true;
    }
}
