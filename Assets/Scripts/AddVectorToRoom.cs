﻿using UnityEngine;
using System.Collections;

public class AddVectorToRoom : MonoBehaviour {
    public Vector2 roomCoordinate;
    public Sprite exitRoomOpen;
    public Sprite exitRoomClosed;
    public int currentColumn;
    private bool doorsSet = false;
    private GameObject leftDoor;
    // Use this for initialization
    void Start () {
  
    }

    // Update is called once per frame
    void Update() {
        if (OrbCount.doorOpen == true && this.gameObject.name == "Exit Room(Clone)") {
            GetComponent<SpriteRenderer>().sprite = exitRoomOpen;
        }

        if (OrbCount.doorOpen == false && this.gameObject.name == "Exit Room(Clone)")
        {
            GetComponent<SpriteRenderer>().sprite = exitRoomClosed;
        }

        if (roomCoordinate == Controls.TopLeftRoomLocation)
        {
            Controls.TopLeftRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.TopMiddleRoomLocation)
        {
            Controls.TopMiddleRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.TopRightRoomLocation)
        {
            Controls.TopRightRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.LeftRoomLocation)
        {
            Controls.LeftRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.RightRoomLocation)
        {
            Controls.RightRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.BottomLeftRoomLocation)
        {
            Controls.BottomLeftRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.BottomMiddleRoomLocation)
        {
            Controls.BottomMiddleRoom = this.gameObject;
        }

        if (roomCoordinate == Controls.BottomRightRoomLocation)
        {
            Controls.BottomRightRoom = this.gameObject;
        }

        if (currentColumn == 4 && doorsSet == false)
         {
            if (this.gameObject.name == "Altar Room(Clone)" || this.gameObject.name == "ButcherRoom(Clone)" ||
                this.gameObject.name == "FireplaceRoom(Clone)")
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                doorsSet = true;
            }
         }

        if (currentColumn == 0 && doorsSet == false)
        {
            if (this.gameObject.name == "Exit Room(Clone)" || this.gameObject.name == "Window Hallway(Clone)")
            {
                transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                doorsSet = true;
            }
        }

    }
}
