using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour {

    public bool isStartingRoom;
    public bool isActiveRoom;
    private PlayerProvvisorio player;

    private SpriteRenderer sr;
    private Door[] door;


	void Start ()
    {
        player = FindObjectOfType<PlayerProvvisorio>();
        sr = GetComponent<SpriteRenderer>();
        door = FindObjectsOfType<Door>();        
    }
	

	void Update ()
    {
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
        if (door.Length > 0)
        {
            foreach (var item in door)
            {
                if (item.nextRoom == this.gameObject)
                {
                    player.transform.position = this.transform.position;
                }
            }            
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("va");
            this.isActiveRoom = true;
        }
    }
}
