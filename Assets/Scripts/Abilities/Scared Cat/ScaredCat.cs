using UnityEngine;
using System.Collections;

public class ScaredCat : MonoBehaviour {
    public Vector2 currentPos;
    public Vector3 facing;
    public Vector3 startingPosition;
    public static Vector2 scaredCatTargetRoomLocation;

    public static GameObject scaredCatTargetRoom;

    private string ability;

    public float scaredyCatSpeed = 165;
    public float coolDown = 15;
    public float timeSinceAbilityUse;
    public float cameraSpeed = 335;

    public bool canUseAbility = true;
    public static bool scaredCatOnLadder = false;
    public static bool scaredCatContinueRunning = true;
    public static bool scaredCatRunning = false;
    public static bool scaredCatGoingDown = false;
    public static bool scaredCatGoingUp = false;

    // Use this for initialization
    void Start ()
    { 
        //set inputs for player
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
        //cooldown manager
        if (canUseAbility == false)
        {
            timeSinceAbilityUse += Time.deltaTime;
            if (timeSinceAbilityUse > coolDown)
            {
                canUseAbility = true;
                timeSinceAbilityUse = 0;
            }
        }

        //allow use of ability if player is in a room with any other player
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

        //while ability is active player runs to target destination at increased player speed and camera speed
        if (scaredCatRunning == true && scaredCatContinueRunning == true)
        {
            //this.transform.position = Vector3.MoveTowards(this.transform.position, scaredCatTargetRoom.transform.position, scaredCatSpeed);
            this.transform.eulerAngles = facing;
            this.transform.Translate(Vector2.right * scaredyCatSpeed * Time.deltaTime);
            this.gameObject.GetComponent<Controls>().cameraSpeed = cameraSpeed;
            this.gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Running", true);
            this.gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Running", true);
            this.gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", false);
            this.gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Idle", false);
            this.gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Jumping", false);
            this.gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Jumping", false); 
        }

  
        //reset camera speed and stop running on arrival of destination
        if (scaredCatTargetRoom.transform.position.x > startingPosition.x)
        {
            if (scaredCatOnLadder) {
                if (this.transform.position.x >= scaredCatTargetRoom.transform.position.x - 240)
                {
                    if (this.transform.position.y >= scaredCatTargetRoom.transform.position.y + 117
                        && scaredCatGoingUp == true)
                    {
                        StopScaredyCat();
                    }

                    if (this.transform.position.y <= scaredCatTargetRoom.transform.position.y -117
                        && scaredCatGoingDown == true)
                    {
                        StopScaredyCat();
                    }
                }
            }


           else if (this.transform.position.x >= scaredCatTargetRoom.transform.position.x)
            {
                StopScaredyCat();
            }
        }

        //reset camera speed and stop running on arrival of destination
        if (scaredCatTargetRoom.transform.position.x < startingPosition.x)
        {
            if (scaredCatOnLadder)
            {
                if (this.transform.position.x <= scaredCatTargetRoom.transform.position.x + 240)
                {
                    if (this.transform.position.y >= scaredCatTargetRoom.transform.position.y + 117
                         && scaredCatGoingUp == true)
                    {
                        StopScaredyCat();
                    }

                    if (this.transform.position.y <= scaredCatTargetRoom.transform.position.y - 117
                        && scaredCatGoingDown == true)
                    {
                        StopScaredyCat();
                    }
                }
            }

            else if (this.transform.position.x <= scaredCatTargetRoom.transform.position.x)
            {
                StopScaredyCat();
            }
        }



    }

    //FUNCTIONS
    //find the target destination when running according to rules of ability and set facing direction
    void FindTargetRoom()
    {
        if (Input.GetButtonDown(ability) && scaredCatRunning == false && canUseAbility == true)
        { 
            scaredCatTargetRoomLocation.y = currentPos.y;
            if (currentPos.x + 2 <= 3)
            {
                scaredCatTargetRoomLocation.x = currentPos.x + 2;
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                facing = new Vector3(0, 0, 0);
                startingPosition = this.transform.position;
            }

            else if (currentPos.x - 2 >= 0)
            {
                scaredCatTargetRoomLocation.x = currentPos.x - 2;
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                facing = new Vector3(0, 180, 0);
                startingPosition = this.transform.position;
            }

           //used for  old 5x5 grid size
           /* else
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
            }*/
            scaredCatRunning = true;
            canUseAbility = false;
        }
    }

    void StopScaredyCat()
    {
        scaredCatRunning = false;
        scaredCatOnLadder = false;
        scaredCatGoingUp = false;
        scaredCatGoingDown = false;
        this.gameObject.GetComponent<Controls>().cameraSpeed = 70;
    }

}
