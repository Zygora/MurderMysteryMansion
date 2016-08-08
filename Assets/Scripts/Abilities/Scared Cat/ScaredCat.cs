using UnityEngine;
using System.Collections;

public class ScaredCat : MonoBehaviour {
    public Vector2 currentPos;
    public static Vector2 scaredCatTargetRoomLocation;
    public static GameObject scaredCatTargetRoom;
    private string ability;
    public float scaredCatSpeed = 5;
    public static bool scaredCatRunning = false;
    public Vector3 facing;
    // Use this for initialization
    void Start ()
    { 
        if (gameObject.tag == "Player1")
        {
            ability = "Ability_P1";
        }

        if (gameObject.tag == "Player2")
        {
            ability = "Ability_P2";
        }

        if (gameObject.tag == "Player3")
        {
            ability = "Ability_P3";
        }

        if (gameObject.tag == "Player4")
        {
            ability = "Ability_P4";
        }
        this.gameObject.GetComponent<Controls>().scaredCat = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player1")
        {
            currentPos = Controls.currentPlayer1Pos;
            if (currentPos == Controls.currentPlayer2Pos || currentPos == Controls.currentPlayer3Pos || 
                currentPos == Controls.currentPlayer4Pos)
            {
                if (Controls.player1NoDropOrbZone == false)
                {
                    FindTargetRoom();
                }
            }
        }

        if (gameObject.tag == "Player2")
        {
            currentPos = Controls.currentPlayer2Pos;
            if (currentPos == Controls.currentPlayer1Pos || currentPos == Controls.currentPlayer3Pos ||
                currentPos == Controls.currentPlayer4Pos)
            {
                if (Controls.player2NoDropOrbZone == false)
                {
                    FindTargetRoom();
                }
            }
        }

        if (gameObject.tag == "Player3")
        {
            currentPos = Controls.currentPlayer3Pos;
            if (currentPos == Controls.currentPlayer1Pos || currentPos == Controls.currentPlayer2Pos ||
               currentPos == Controls.currentPlayer4Pos)
            {
                if (Controls.player3NoDropOrbZone == false)
                {
                    FindTargetRoom();
                }
            }
        }

        if (gameObject.tag == "Player4")
        {
            currentPos = Controls.currentPlayer4Pos;
            if (currentPos == Controls.currentPlayer1Pos || currentPos == Controls.currentPlayer2Pos ||
               currentPos == Controls.currentPlayer3Pos)
            {
                if (Controls.player4NoDropOrbZone == false)
                {
                    FindTargetRoom();
                }
            }
        }

        if(scaredCatRunning == true)
        {
           this.transform.position = Vector3.MoveTowards(this.transform.position, scaredCatTargetRoom.transform.position, scaredCatSpeed);
           this.gameObject.GetComponent<Controls>().cameraSpeed = 450;
           this.gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Running", true);
           this.gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Running", true);
           this.gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", false);
           this.gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Idle", false);
           this.gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Jumping", false);
           this.gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Jumping", false);
            this.transform.eulerAngles = facing;
        }

        if(this.transform.position == scaredCatTargetRoom.transform.position)
        {
            scaredCatRunning = false;
            this.gameObject.GetComponent<Controls>().cameraSpeed = 70;
        }
    }

    void FindTargetRoom()
    {
        if (Input.GetButtonDown(ability) && scaredCatRunning == false)
        { 
            scaredCatTargetRoomLocation.y = currentPos.y;
            if (currentPos.x + 3 <= 2)
            {
                scaredCatTargetRoomLocation.x = currentPos.x + 3;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                facing = new Vector3(0, 0, 0);
            }

            else if (currentPos.x - 3 >= -2)
            {
                scaredCatTargetRoomLocation.x = currentPos.x - 3;
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                facing = new Vector3(0, 180, 0);
            }

            else
            {
                int x = Random.Range(1, 3);
                if (currentPos.x < 0)
                {
                    scaredCatTargetRoomLocation.x = 2;
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    facing = new Vector3(0, 0, 0);
                }

                if (currentPos.x > 0)
                {
                    scaredCatTargetRoomLocation.x = -2;
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                    facing = new Vector3(0, 180, 0);
                }

                if (x == 1 && currentPos.x == 0)
                {
                    scaredCatTargetRoomLocation.x = 2;
                    this.transform.eulerAngles = new Vector3(0, 0, 0);
                    facing = new Vector3(0, 0, 0);
                }

                if (x == 2 && currentPos.x == 0)
                {
                    scaredCatTargetRoomLocation.x = -2;
                    this.transform.eulerAngles = new Vector3(0, 180, 0);
                    facing = new Vector3(0, 180, 0);
                }
            }
            scaredCatRunning = true;
            Debug.Log("scared cat");
            Debug.Log(scaredCatTargetRoomLocation);
            Debug.Log(scaredCatRunning);
            Debug.Log(scaredCatTargetRoom.name);
        }
    }
}
