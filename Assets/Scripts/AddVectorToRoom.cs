using UnityEngine;
using System.Collections;

public class AddVectorToRoom : MonoBehaviour {
    public Vector2 roomCoordinate;
    public Sprite exitRoomOpen;
    public Sprite exitRoomClosed;
    public int currentColumn;
    private int doorsSet = 0;
    private GameObject leftDoor;
    // Use this for initialization
    void Start () {
  
    }
	
	// Update is called once per frame
	void Update () {
        if (OrbCount.doorOpen == true && this.gameObject.name == "Exit Room(Clone)") {
            GetComponent<SpriteRenderer>().sprite = exitRoomOpen;
        }

        if (OrbCount.doorOpen == false && this.gameObject.name == "Exit Room(Clone)")
        {
            GetComponent<SpriteRenderer>().sprite = exitRoomClosed;
        }

        if (currentColumn == 4 && this.gameObject.name == "Exit Room(Clone)" && doorsSet == 0)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            doorsSet ++;
        }
    }
}
