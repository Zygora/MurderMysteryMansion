﻿using UnityEngine;
using System.Collections;

public class AddVectorToRoom : MonoBehaviour
{
    public Vector2 roomCoordinate;

    public Sprite exitRoomOpen;
    public Sprite exitRoomClosed;

    public int currentColumn;
    private int exitDoorChange = 0;

    private bool doorsSet = false;

    private GameObject leftDoor;


    // Update is called once per frame
    void Update()
    {
        //change sprite of exit room depending on whether the door is opened or not
        if (OrbCount.doorOpen == true && this.gameObject.name == "Exit Room(Clone)" && exitDoorChange == 1)
        {
            GetComponent<SpriteRenderer>().sprite = exitRoomOpen;
            SwitchExitDoors(1, 3, 5, 6);
            exitDoorChange = 0;
        }

        if (OrbCount.doorOpen == false && this.gameObject.name == "Exit Room(Clone)" && exitDoorChange == 0)
        {
            GetComponent<SpriteRenderer>().sprite = exitRoomClosed;
            SwitchExitDoors(0, 2, 4, 4);
            exitDoorChange = 1;
        }


        //grab this room position and send it to scaredy cat script if it is the target location
        if (roomCoordinate == ScaredCat.scaredCatTargetRoomLocation)
        {
            ScaredCat.scaredCatTargetRoom = this.gameObject;
        }

        //get adjacent room positions and send them to controls sript for use with crazed alchemist
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

        //enable left door sprite of room 
        if (currentColumn == 3 && doorsSet == false)
        {
            if (this.gameObject.name != "Exit Room(Clone)")
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                doorsSet = true;
            }
        }

        //enable sprite right door of room
        if (currentColumn == 0 && doorsSet == false)
        {
            if (this.gameObject.name != "Hall of Portraits(Clone)" && this.gameObject.name != "Library(Clone)"
                && this.gameObject.name != "Exit Room(Clone)")
            {
                transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
            }

            else if (this.gameObject.name != "Exit Room(Clone)")
            {
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }

            doorsSet = true;
        }

        //enable double door sprites
        if (doorsSet == false)
        {
            if (currentColumn != 0 || currentColumn != 3)
            {
                if (this.gameObject.name != "Hall of Portraits(Clone)" && this.gameObject.name != "Library(Clone)" &&
                   this.gameObject.name != "Hall of Portraits(Second Half)(Clone)"
                   && this.gameObject.name != "Library(Second Half)(Clone)" && this.gameObject.name != "Exit Room(Clone)")
                {
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                    transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
                    transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
                    transform.GetChild(3).GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }

    }

    void SwitchExitDoors(int child1, int child2, int child3, int child4) {
        if (currentColumn == 3)
        {
            transform.GetChild(child1).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(child3).GetComponent<SpriteRenderer>().enabled = true;
        }

        else if (currentColumn == 0)
        {
            transform.GetChild(child2).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(child4).GetComponent<SpriteRenderer>().enabled = true;
        }

        else if (currentColumn != 0 || currentColumn != 3)
        {
            transform.GetChild(child1).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(child2).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(child3).GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(child4).GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
