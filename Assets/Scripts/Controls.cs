using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Controls : MonoBehaviour
{
    // Tweakable variables
    [Header("Tweakable variables")]
    public float playerSpeed;
    public float playerJumpForce;
    public float playerClimbSpeed;
    public int speedWhileCarryOrb = 40;
    public int speedWhileNotCarryOrb = 55;
    // System variables
    private Rigidbody2D rb;
    private Vector3 move;
    // References to other objects (leave public)
    [Header("References")]

    public GameObject groundCheck;

    [Space(10)]
    // Ladder booleans (make private at the end)
    [Header("Ladder booleans")]
    public bool onLadder;
    public bool onGround;
    //public bool onNoJumpArea;
    public bool canGoUp;
    public bool canGoDown;
    //
    public SpriteRenderer GateHighlight;
    public Camera Camera;
    public GameObject player1Camera;
    public GameObject player2Camera;
    public GameObject player3Camera;
    public GameObject player4Camera;
    public GameObject OptionsRoom;
    public GameObject PlayerCustomizationRoom;
    public GameObject MenuRoom;
    public Animator TorsoAnimator;
    public Animator LegsAnimator;
    public Animator ShirtAnimator;
    private Collider2D col;

    public bool moveCameraLeft;
    public bool moveCameraRight;
    public bool moveCameraToCenter;

    public float MenuDownHoldTime;
    public float playerTransitionSpeed;

    public string horizontal;
    public string vertical;
    public string revive1;
    public string revive2;
    public string revive3;
    public string revive4;

    public string jump;
    public bool canMove = true;
    public float cameraSpeed = 30;
    public float direction = 1;
    bool speedIncreased;
    float timeSpeedIncreased;
    private bool dead;
    public bool moveCamera;
    public GameObject currentRoom;
    public GameObject lastRoom;
    float speedMultiplier;
    public bool goUp;
    public bool goDown;
    private float gravityScale;
    private GameObject ladder;
    private GameObject[] Shirts;
    public static bool murderTransitioning = false;
    public static bool bloodStained = false;

    public static bool wimpKilled = false;
    public static int wimpsDowned = 0;
    private string interact;
    public bool exited;
    private float wimpDownedCooldown;
    private float wimpDownedDelay = 1;
    public static Vector2 currentPlayer1Pos;
    public static Vector2 currentPlayer2Pos;
    public static Vector2 currentPlayer3Pos;
    public static Vector2 currentPlayer4Pos;
    float revivetime = 2.0f;
    float revivetimepassed = 0.0f;   
    public string revive;
    public int MurdererWeaponCooldown;
    public static bool player1NoDropOrbZone = false;
    public static bool player2NoDropOrbZone = false;
    public static bool player3NoDropOrbZone = false;
    public static bool player4NoDropOrbZone = false;
    public bool diseased;
    public float thrillSpeedBoost;
    public bool drNerd;
    public bool crazedAlchemist;
    public Vector2 currentPos;
    private bool teleported = false;
    //teleport locations
    public static GameObject TopLeftRoom;
    public static GameObject TopMiddleRoom;
    public static GameObject TopRightRoom;
    public static GameObject LeftRoom;
    public static GameObject RightRoom;
    public static GameObject BottomLeftRoom;
    public static GameObject BottomMiddleRoom;
    public static GameObject BottomRightRoom;
    public static Vector2 TopLeftRoomLocation;
    public static Vector2 TopMiddleRoomLocation;
    public static Vector2 TopRightRoomLocation;
    public static Vector2 LeftRoomLocation;
    public static Vector2 RightRoomLocation;
    public static Vector2 BottomLeftRoomLocation;
    public static Vector2 BottomMiddleRoomLocation;
    public static Vector2 BottomRightRoomLocation;



    void Start()
    {
        // Save players gravity scale
        gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        // Get rigidbody of the player at start
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        //Animator = GetComponent<Animator>();
        // Time that player should hold down button near the gates in order to exit the game
        MenuDownHoldTime = 0;
        // Spped of the player when he runs from screen to screen while transitioning
        playerTransitionSpeed = playerSpeed / 4;
        dead = false;
        //grab cameras in scene
        player1Camera = GameObject.FindGameObjectWithTag("Player1Camera");
        player2Camera = GameObject.FindGameObjectWithTag("Player2Camera");
        player3Camera = GameObject.FindGameObjectWithTag("Player3Camera");
        player4Camera = GameObject.FindGameObjectWithTag("Player4Camera");
        //set input in input manager
        if (gameObject.tag == "Player1")
        {
            interact = "Interact_P1";

            revive = "Attack/Revive_P1";
        }

        if (gameObject.tag == "Player2")
        {
            interact = "Interact_P2";
            revive = "Attack/Revive_P2";
        }

        if (gameObject.tag == "Player3")
        {
            interact = "Interact_P3";
            revive = "Attack/Revive_P3";
        }

        if (gameObject.tag == "Player4")
        {
            interact = "Interact_P4";
            revive = "Attack/Revive_P4";
        }
        
    }

    void Update()
    {
        //track positions of player
        if (crazedAlchemist == true)
        {
            if (gameObject.tag == "Player1")
            {
                currentPos = currentPlayer1Pos;
                GetTeleportLocations();
            }
            if (gameObject.tag == "Player2")
            {
                currentPos = currentPlayer2Pos;
                GetTeleportLocations();
            }
            if (gameObject.tag == "Player3")
            {
                currentPos = currentPlayer3Pos;
                GetTeleportLocations();
            }
            if (gameObject.tag == "Player4")
            {
                currentPos = currentPlayer4Pos;
                GetTeleportLocations();
            }
        }

        if (GameOverTextManager.gameOver == true) {
            playerSpeed = 0;
        }

        if (GameOverTextManager.gameOver == false)
        {
            if (gameObject.tag == "Player1" || gameObject.tag == "Player2" || gameObject.tag == "Player3" || gameObject.tag == "Player4")
            {
                dead = false;
                TorsoAnimator.SetBool("Dead", false);
                LegsAnimator.SetBool("Dead", false);
            }
            //disable sprite renderer if wimp has exited
            if (gameObject.tag == "Player1" && OrbCount.player1Exited == true)
            {
                //make exited player not visible to the camera
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50);
                //turn player speed to 0 preventing movement
                playerSpeed = 0;
                //make player to prevent player from falling through floor
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //disable player colliders
                gameObject.GetComponent<Collider2D>().enabled = false;
                groundCheck.GetComponent<Collider2D>().enabled = false;
                exited = true;

            }

            if (gameObject.tag == "Player2" && OrbCount.player2Exited == true)
            {
                //make exited player not visible to the camera
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50);
                //turn player speed to 0 preventing movement
                playerSpeed = 0;
                //make player to prevent player from falling through floor
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //disable player colliders
                gameObject.GetComponent<Collider2D>().enabled = false;
                groundCheck.GetComponent<Collider2D>().enabled = false;
                exited = true;
            }

            if (gameObject.tag == "Player3" && OrbCount.player3Exited == true)
            {
                //make exited player not visible to the camera
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50);
                //turn player speed to 0 preventing movement
                playerSpeed = 0;
                //make player to prevent player from falling through floor
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //disable player colliders
                gameObject.GetComponent<Collider2D>().enabled = false;
                groundCheck.GetComponent<Collider2D>().enabled = false;
                exited = true;
            }

            if (gameObject.tag == "Player4" && OrbCount.player4Exited == true)
            {
                //make exited player not visible to the camera
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50);
                //turn player speed to 0 preventing movement
                playerSpeed = 0;
                //make player to prevent player from falling through floor
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                //disable player colliders
                gameObject.GetComponent<Collider2D>().enabled = false;
                groundCheck.GetComponent<Collider2D>().enabled = false;
                exited = true;
            }
            //enable sprite renderer if wimp has re entered
            if (gameObject.tag == "Player1" && OrbCount.player1Exited == true && Input.GetButtonDown(interact) && OrbCount.player1CanEnter == true)
            {
                //make player visible to camera again
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                //restore player speed
                playerSpeed = speedWhileNotCarryOrb;
                //play idle animation on re entry
                TorsoAnimator.SetBool("Running", false);
                LegsAnimator.SetBool("Running", false);
                TorsoAnimator.SetBool("Idle", true);
                LegsAnimator.SetBool("Idle", true);
                //restore rigidbody parameters to normal
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //enable player colliders
                gameObject.GetComponent<Collider2D>().enabled = true;
                groundCheck.GetComponent<Collider2D>().enabled = true;
                //reset values to false so that player can exit again and re enter after exiting
                OrbCount.player1Exited = false;
                OrbCount.player1CanEnter = false;
                //decrease wimps exited by 1 when player re enters
                OrbCount.wimpsExited -= 1;
                exited = false;
            }

            if (gameObject.tag == "Player2" && OrbCount.player2Exited == true && Input.GetButtonDown(interact) && OrbCount.player2CanEnter == true)
            {
                //make player visible to camera again
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                //restore player speed
                playerSpeed = speedWhileNotCarryOrb;
                //play idle animation on re entry
                TorsoAnimator.SetBool("Running", false);
                LegsAnimator.SetBool("Running", false);
                TorsoAnimator.SetBool("Idle", true);
                LegsAnimator.SetBool("Idle", true);
                //restore rigidbody parameters to normal
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //enable player colliders
                gameObject.GetComponent<Collider2D>().enabled = true;
                groundCheck.GetComponent<Collider2D>().enabled = true;
                //reset values to false so that player can exit again and re enter after exiting
                OrbCount.player1Exited = false;
                OrbCount.player1CanEnter = false;
                //decrease wimps exited by 1 when player re enters
                OrbCount.wimpsExited -= 1;
                Debug.Log("wimp entered");
                exited = false;
            }

            if (gameObject.tag == "Player3" && OrbCount.player3Exited == true && Input.GetButtonDown(interact) && OrbCount.player3CanEnter == true)
            {
                //make player visible to camera again
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                //restore player speed
                playerSpeed = speedWhileNotCarryOrb;
                //play idle animation on re entry
                TorsoAnimator.SetBool("Running", false);
                LegsAnimator.SetBool("Running", false);
                TorsoAnimator.SetBool("Idle", true);
                LegsAnimator.SetBool("Idle", true);
                //restore rigidbody parameters to normal
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //enable player colliders
                gameObject.GetComponent<Collider2D>().enabled = true;
                groundCheck.GetComponent<Collider2D>().enabled = true;
                //reset values to false so that player can exit again and re enter after exiting
                OrbCount.player1Exited = false;
                OrbCount.player1CanEnter = false;
                //decrease wimps exited by 1 when player re enters
                OrbCount.wimpsExited -= 1;
                Debug.Log("wimp entered");
                exited = false;
            }

            if (gameObject.tag == "Player4" && OrbCount.player4Exited == true && Input.GetButtonDown(interact) && OrbCount.player4CanEnter == true)
            {
                //make player visible to camera again
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
                //restore player speed
                playerSpeed = speedWhileNotCarryOrb;
                //play idle animation on re entry
                TorsoAnimator.SetBool("Running", false);
                LegsAnimator.SetBool("Running", false);
                TorsoAnimator.SetBool("Idle", true);
                LegsAnimator.SetBool("Idle", true);
                //restore rigidbody parameters to normal
                gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                //enable player colliders
                gameObject.GetComponent<Collider2D>().enabled = true;
                groundCheck.GetComponent<Collider2D>().enabled = true;
                //reset values to false so that player can exit again and re enter after exiting
                OrbCount.player1Exited = false;
                OrbCount.player1CanEnter = false;
                OrbCount.wimpsExited -= 1;
                //decrease wimps exited by 1 when player re enters
                Debug.Log("wimp entered");
                exited = false;
            }

            //decrease speed of player if carrying an orb
            if (gameObject.tag == "Player1" && OrbCount.player1CarryOrb == true)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileCarryOrb;
                }

                if (speedIncreased == true)
                {
                    playerSpeed = speedWhileCarryOrb*2;
                }
            }

            if (gameObject.tag == "Player2" && OrbCount.player2CarryOrb == true)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileCarryOrb;
                }

                if (speedIncreased == true)
                {
                    playerSpeed = speedWhileCarryOrb * 2;
                }
            }

            if (gameObject.tag == "Player3" && OrbCount.player3CarryOrb == true)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileCarryOrb;
                }

                if (speedIncreased == true)
                {
                    playerSpeed = speedWhileCarryOrb * 2;
                }
            }

            if (gameObject.tag == "Player4" && OrbCount.player4CarryOrb == true)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileCarryOrb;
                }

                if (speedIncreased == true)
                {
                    playerSpeed = speedWhileCarryOrb * 2;
                }
            }

            //restore speed of player if not carrying an orb
            if (gameObject.tag == "Player1" && OrbCount.player1CarryOrb == false)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileNotCarryOrb;
                }
            }

            if (gameObject.tag == "Player2" && OrbCount.player2CarryOrb == false)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileNotCarryOrb;
                }
            }

            if (gameObject.tag == "Player3" && OrbCount.player3CarryOrb == false)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileNotCarryOrb;
                }
            }

            if (gameObject.tag == "Player4" && OrbCount.player4CarryOrb == false)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileNotCarryOrb;
                };
            }
        }

            if (speedIncreased)
            {
                timeSpeedIncreased += Time.deltaTime;
                if (timeSpeedIncreased > 3)
                {
                    playerSpeed /= 2;
                    timeSpeedIncreased = 0;
                    speedIncreased = false;
                }
            }

            if (canMove && dead == false && exited == false)
            {
            if (GameOverTextManager.gameOver == false)
            {
                //turn off movement of murderer during certain times
                if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                {
                    if (ShittyPossum.possumed == true || MurdererScripts.washingClothes == true || MurdererScripts.diseased == true)
                    {
                        playerSpeed = 0;
                    }

                    if (MurdererScripts.thrill == true) {
                        playerSpeed = thrillSpeedBoost + speedWhileNotCarryOrb;
                    }
                    //restore movement of movement after
                    if (ShittyPossum.possumed == false && MurdererScripts.washingClothes == false && MurdererScripts.diseased == false 
                        && MurdererScripts.thrill == false && speedIncreased == false)
                    {
                        playerSpeed = speedWhileNotCarryOrb;
                    }
                }
            }
                // Create a ray down checking if there is anything underneath the player
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
                // If ray hit something player is on ground
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "Ground" && hit.distance < 12f)//1.3
                    {
                        onGround = true;
                        TorsoAnimator.SetBool("Jumping", false);
                        LegsAnimator.SetBool("Jumping", false);
                    if (gameObject.tag == "Player1")
                    {
                        player1NoDropOrbZone = false;
                    }

                    if (gameObject.tag == "Player2")
                    {
                        player2NoDropOrbZone = false;
                    }

                    if (gameObject.tag == "Player3")
                    {
                        player3NoDropOrbZone = false;
                    }

                    if (gameObject.tag == "Player4")
                    {
                        player4NoDropOrbZone = false;
                    }
                }
                    // otherwise the player is in the air
                    else if (hit.distance >= 12f)//1.3
                    {
                        onGround = false;
                        TorsoAnimator.SetBool("Jumping", true);
                        LegsAnimator.SetBool("Jumping", true);
                    if (gameObject.tag == "Player1")
                    {
                        player1NoDropOrbZone = true;
                    }

                    if (gameObject.tag == "Player2")
                    {
                        player2NoDropOrbZone = true;
                    }

                    if (gameObject.tag == "Player3")
                    {
                        player3NoDropOrbZone = true;
                    }

                    if (gameObject.tag == "Player4")
                    {
                        player4NoDropOrbZone = true;
                    }
                }
                }
                // Create move vector
                move = new Vector3(Input.GetAxis(horizontal), 0, 0);
                //set movement bounds on player
                if (transform.position.x <= -580)
                {
                    transform.position = new Vector3(-570, transform.position.y, transform.position.z);
                }

                if (transform.position.x >= 580)
                {
                    transform.position = new Vector3(570, transform.position.y, transform.position.z);
                }

                if (transform.position.y <= -337.5f)
                {
                    transform.position = new Vector3(transform.position.x, -327.5f, transform.position.z);
                }

                if (transform.position.y >= 337.5f)
                {
                    transform.position = new Vector3(transform.position.x, 327.5f, transform.position.z);
                }

                // Move the player
                transform.position += move * playerSpeed * Time.deltaTime;

           //murderer animations
            if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
            {

                if (murderTransitioning != true)
                {
                    //play idle animation
                    if (Input.GetAxis(horizontal) > -0.5f && Input.GetAxis(horizontal) < 0.5f)
                    {
                        TorsoAnimator.SetBool("MurdererRunning", false);
                        LegsAnimator.SetBool("Running", false);
                        TorsoAnimator.SetBool("Idle", true);
                        LegsAnimator.SetBool("Idle", true);
                        ShirtAnimator.SetBool("MurdererRunning", false);
                        ShirtAnimator.SetBool("MurdererIdle", true);
                    }

                    // Change animation from idle to run and flip the players sprite
                    if (Input.GetAxis(horizontal) < -0.1f)
                    {
                        if (playerSpeed != 0)
                        {
                            TorsoAnimator.SetBool("MurdererRunning", true);
                            LegsAnimator.SetBool("Running", true);
                            TorsoAnimator.SetBool("Idle", false);
                            LegsAnimator.SetBool("Idle", false);
                            transform.eulerAngles = new Vector3(0, 180, 0);
                            ShirtAnimator.SetBool("MurdererRunning", true);
                            ShirtAnimator.SetBool("MurdererIdle", false);
                        }
                        direction = -1;
                    }
                    // Change animation from idle to run and flip the sprite
                    if (Input.GetAxis(horizontal) > 0.1f)
                    {
                        if (playerSpeed != 0)
                        {
                            TorsoAnimator.SetBool("MurdererRunning", true);
                            LegsAnimator.SetBool("Running", true);
                            TorsoAnimator.SetBool("Idle", false);
                            LegsAnimator.SetBool("Idle", false);
                            transform.eulerAngles = new Vector3(0, 0, 0);
                            ShirtAnimator.SetBool("MurdererRunning", true);
                            ShirtAnimator.SetBool("MurdererIdle", false);
                        }
                        direction = 1;
                    }
                }

                else {
                    ShirtAnimator.SetBool("MurdererIdle", false);
                    ShirtAnimator.SetBool("MurdererJumping", false);
                }
            }

            //player animations
           if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" && gameObject.tag != "Murderer4")
           {
                //play idle animation
                if (Input.GetAxis(horizontal) > -0.5f && Input.GetAxis(horizontal) < 0.5f)
                {
                    TorsoAnimator.SetBool("Running", false);
                    LegsAnimator.SetBool("Running", false);
                    TorsoAnimator.SetBool("Idle", true);
                    LegsAnimator.SetBool("Idle", true);
                }
                // Change animation from idle to run and flip the players sprite
                if (Input.GetAxis(horizontal) < -0.1f)
                {
                    if (playerSpeed != 0)
                    {
                        TorsoAnimator.SetBool("Running", true);
                        LegsAnimator.SetBool("Running", true);
                        TorsoAnimator.SetBool("Idle", false);
                        LegsAnimator.SetBool("Idle", false);
                        transform.eulerAngles = new Vector3(0, 180, 0);
                    }
                    direction = -1;
                }
                // Change animation from idle to run and flip the sprite
                if (Input.GetAxis(horizontal) > 0.1f)
                {
                    if (playerSpeed != 0)
                    {
                        TorsoAnimator.SetBool("Running", true);
                        LegsAnimator.SetBool("Running", true);
                        TorsoAnimator.SetBool("Idle", false);
                        LegsAnimator.SetBool("Idle", false);
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    direction = 1;
                }
           }
                // If player is on ground and space button hit -> jump
                if (Input.GetButtonDown(jump) && (onGround))
                {
                    rb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                }
                if (Input.GetKeyUp(KeyCode.R))
                {
                    SceneManager.LoadScene(4); // load level generator
                }
            }
        
        //go to main menu
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if (moveCameraLeft || moveCameraRight || moveCameraToCenter)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
        // Transitioning between screen in the menu
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (moveCameraLeft == true)
            {
                Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, OptionsRoom.transform.position, cameraSpeed * Time.deltaTime);
                canMove = false;
                transform.position += Vector3.left * playerTransitionSpeed * Time.deltaTime;
                if (Camera.transform.position == OptionsRoom.transform.position)
                {
                    moveCameraLeft = false;
                    canMove = true;
                }
            }
            if (moveCameraRight == true)
            {
                Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, PlayerCustomizationRoom.transform.position, cameraSpeed * Time.deltaTime);
                canMove = false;
                transform.position += Vector3.right * playerTransitionSpeed * Time.deltaTime;
                if (Camera.transform.position == PlayerCustomizationRoom.transform.position)
                {
                    moveCameraRight = false;
                }
            }
            if (moveCameraToCenter == true)
            {
                Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, MenuRoom.transform.position, cameraSpeed * Time.deltaTime);
                canMove = false;
                Vector3 direction;
                direction = new Vector3(MenuRoom.transform.position.x - gameObject.transform.position.x, 0, 0);
                direction.Normalize();
                transform.position += direction * playerSpeed / 4 * Time.deltaTime;
                if (Camera.transform.position == MenuRoom.transform.position)
                {
                    moveCameraToCenter = false;
                }
            }
        }

        // if player hits upArrow on the ladder go up
        if (Input.GetAxis(vertical) > 0 && (canGoUp))
        {
            goUp = true;
            gameObject.transform.position = new Vector3(ladder.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        // Go up
        if (goUp)
        {
            canMove = false;
            groundCheck.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position += new Vector3(0, 1, 0) * playerClimbSpeed * Time.deltaTime * speedMultiplier;
        }

        // if player hits downArrow on the ladder go up
        if (Input.GetAxis(vertical) < 0 && (canGoDown))
        {
            goDown = true;
            gameObject.transform.position = new Vector3(ladder.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        // Go down
        if (goDown)
        {
            canMove = false;
            groundCheck.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position += new Vector3(0, -1, 0) * playerClimbSpeed * Time.deltaTime * speedMultiplier;
        }
        // Change player animation to jump while transitioning up or down
        if (goDown || goUp)
        {
            TorsoAnimator.SetBool("Jumping", true);
            LegsAnimator.SetBool("Jumping", true);
            if (gameObject.tag == "Player1")
            {
                player1NoDropOrbZone = true;
            }

            if (gameObject.tag == "Player2")
            {
                player2NoDropOrbZone = true;
            }

            if (gameObject.tag == "Player3")
            {
                player3NoDropOrbZone = true;
            }

            if (gameObject.tag == "Player4")
            {
                player4NoDropOrbZone = true;
            }
            if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
            {
                ShirtAnimator.SetBool("MurdererJumping", true);
            }
        }
        // If camera transitioning between rooms
        if (moveCamera)
        {
            Vector3 nextRoomCamPos = currentRoom.transform.position;
            Vector3 direction;
            direction = new Vector3(currentRoom.transform.position.x - gameObject.transform.position.x, 0, 0);
            direction.Normalize();
            nextRoomCamPos.z = -10;
            //change player camera according to player
            if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1")
            {
                player1Camera.transform.position = Vector3.MoveTowards(player1Camera.transform.position, nextRoomCamPos, cameraSpeed * Time.deltaTime);
            }

            if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
            {
                player2Camera.transform.position = Vector3.MoveTowards(player2Camera.transform.position, nextRoomCamPos, cameraSpeed * Time.deltaTime);
            }

            if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
            {
                player3Camera.transform.position = Vector3.MoveTowards(player3Camera.transform.position, nextRoomCamPos, cameraSpeed * Time.deltaTime);
            }

            if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
            {
                player4Camera.transform.position = Vector3.MoveTowards(player4Camera.transform.position, nextRoomCamPos, cameraSpeed * Time.deltaTime);
            }
            canMove = false;
            // Change player animation to run while transitioning left or right
            if ((!goUp) && (!goDown))
            {
                if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" && gameObject.tag != "Murderer4")
                {

                    TorsoAnimator.SetBool("Running", true);
                    LegsAnimator.SetBool("Running", true);
                    TorsoAnimator.SetBool("Idle", false);
                    LegsAnimator.SetBool("Idle", false);
                    if (gameObject.tag == "Player1")
                    {
                        player1NoDropOrbZone = true;
                    }

                    if (gameObject.tag == "Player2")
                    {
                        player2NoDropOrbZone = true;
                    }

                    if (gameObject.tag == "Player3")
                    {
                        player3NoDropOrbZone = true;
                    }

                    if (gameObject.tag == "Player4")
                    {
                        player4NoDropOrbZone = true;
                    }
                }
                
                if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                {
                    murderTransitioning = true;
                    TorsoAnimator.SetBool("MurdererRunning", true);
                    LegsAnimator.SetBool("Running", true);
                }
                
                transform.position += direction * playerSpeed / 4 * Time.deltaTime * speedMultiplier;
            }
            if (Input.GetAxis(horizontal) < 0 && (direction.x == 1) && (!goDown) && (!goUp))
            {
                GameObject buffer;
                buffer = currentRoom;
                currentRoom = lastRoom;
                lastRoom = buffer;
                direction *= -1;
                canMove = false;
                transform.eulerAngles = new Vector3(0, 180, 0);
                if (speedMultiplier <= 2)
                {
                    speedMultiplier = 2;
                }
            }
            if (Input.GetAxis(horizontal) > 0 && (direction.x == -1) && (!goDown) && (!goUp))
            {
                GameObject buffer;
                buffer = currentRoom;
                currentRoom = lastRoom;
                lastRoom = buffer;
                direction *= -1;
                canMove = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
                if (speedMultiplier <= 2)
                {
                    speedMultiplier = 2;
                }
            }

            if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1")
            {
                if ((player1Camera.transform.position.x == currentRoom.transform.position.x)
                    && (player1Camera.transform.position.y == currentRoom.transform.position.y))
                {
                    if (speedMultiplier != 1)
                    {
                        speedMultiplier = 1;
                    }
                    moveCamera = false;
                    canMove = true;
                    player1NoDropOrbZone = false;
                    if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                    {
                        murderTransitioning = false;
                    }
                    }
            }

            if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
            {
                if ((player2Camera.transform.position.x == currentRoom.transform.position.x)
                    && (player2Camera.transform.position.y == currentRoom.transform.position.y))
                {
                    if (speedMultiplier != 1)
                    {
                        speedMultiplier = 1;
                    }
                    moveCamera = false;
                    canMove = true;
                    player2NoDropOrbZone = false;
                    if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                    {
                        murderTransitioning = false;
                    }
                }
            }

            if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
            {
                if ((player3Camera.transform.position.x == currentRoom.transform.position.x)
                    && (player3Camera.transform.position.y == currentRoom.transform.position.y))
                {
                    if (speedMultiplier != 1)
                    {
                        speedMultiplier = 1;
                    }
                    moveCamera = false;
                    canMove = true;
                    player3NoDropOrbZone = false;
                    if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                    {
                        murderTransitioning = false;
                    }
                }
            }

            if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
            {
                if ((player4Camera.transform.position.x == currentRoom.transform.position.x)
                    && (player4Camera.transform.position.y == currentRoom.transform.position.y))
                {
                    if (speedMultiplier != 1)
                    {
                        speedMultiplier = 1;
                    }
                    moveCamera = false;
                    canMove = true;
                    player4NoDropOrbZone = false;
                    if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                    {
                        murderTransitioning = false;
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        // While player is at the bottom of a ladder they can't go down and can go up
        if (other.gameObject.tag == "BotLadder")
        {
            ladder = other.gameObject;
            canGoDown = false;
            canGoUp = true;
        }
        // At the top of a ladder turn off the collision of a player and the platform above them
        if (other.tag == "TopLadder")
        {
            ladder = other.gameObject;
            canGoDown = true;
            canGoUp = false;
        }
        //change highlight on play button and change level on button input
        if (other.gameObject.tag == "Gate")
        {
            GateHighlight.enabled = true;
            if (Input.GetKey(KeyCode.UpArrow))
            {

            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                MenuDownHoldTime += Time.deltaTime;
            }
            if (MenuDownHoldTime > 5)
            {
                SceneManager.LoadScene(4);
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                MenuDownHoldTime = 0;
            }
        }

        //set coordinates of player according to vector grid
        if (other.gameObject.tag == "Room" || other.gameObject.tag == "MurdererStart")
        {
            if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1") {
                currentPlayer1Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
            }

            if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
            {
                currentPlayer2Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
            }

            if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
            {
                currentPlayer3Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
            }

            if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
            {
                currentPlayer4Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
            }
        }

        if (other.gameObject.tag == "DownedWimp1" || other.gameObject.tag == "DownedWimp2" || other.gameObject.tag == "DownedWimp3" || other.gameObject.tag == "DownedWimp4")
        {
            if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" && gameObject.tag != "Murderer4")
            {
                if (Input.GetButton(revive))
                {
                    TorsoAnimator.SetBool("Reviving", true);
                    LegsAnimator.SetBool("Reviving", true);
                    Debug.Log("Reviving");
                    other.gameObject.layer = 11;
                    Debug.Log("Revive Time Passed:" + revivetimepassed);
                    revivetimepassed += Time.deltaTime;
                    if (drNerd == true)
                    {
                        revivetime = 0;
                    }
                    if (revivetimepassed > revivetime)
                    {
                        TorsoAnimator.SetBool("Reviving", false);
                        LegsAnimator.SetBool("Reviving", false);
                        other.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
                        Debug.Log("Reviving Done");
                        switch (other.gameObject.tag)
                        {
                            case "DownedWimp1":
                                other.gameObject.tag = "Player1";
                                GameObject.FindGameObjectWithTag("Player1").transform.GetChild(2).GetComponent<BoxCollider2D>().isTrigger = false;
                                break;
                            case "DownedWimp2":
                                other.gameObject.tag = "Player2";
                                GameObject.FindGameObjectWithTag("Player2").transform.GetChild(2).GetComponent<BoxCollider2D>().isTrigger = false;
                                break;
                            case "DownedWimp3":
                                other.gameObject.tag = "Player3";
                                GameObject.FindGameObjectWithTag("Player3").transform.GetChild(2).GetComponent<BoxCollider2D>().isTrigger = false;
                                break;
                            case "DownedWimp4":
                                other.gameObject.tag = "Player4";
                                GameObject.FindGameObjectWithTag("Player4").transform.GetChild(2).GetComponent<BoxCollider2D>().isTrigger = false;
                                break;
                        }
                        revivetimepassed = 0;
                        wimpsDowned -= 1;
                    }
                    
                }
            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ColorSelection")
        {
            GameObject.Find("ColorManager").GetComponent<ColorManager>().playerCanSelect = true;
        }
        if (other.tag == "BotLadder")
        {
            canMove = true;
            goDown = false;
            TorsoAnimator.SetBool("Jumping", false);
            LegsAnimator.SetBool("Jumping", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            groundCheck.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            if (gameObject.tag == "Player1")
            {
                player1NoDropOrbZone = false;
            }

            if (gameObject.tag == "Player2")
            {
                player2NoDropOrbZone = false;
            }

            if (gameObject.tag == "Player3")
            {
                player3NoDropOrbZone = false;
            }

            if (gameObject.tag == "Player4")
            {
                player4NoDropOrbZone = false;
            }
        }
        if (other.tag == "TopLadder")
        {
            canMove = true;
            goUp = false;
            TorsoAnimator.SetBool("Jumping", false);
            LegsAnimator.SetBool("Jumping", false);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            // gameObject.GetComponent<Collider2D>().enabled = true;
            groundCheck.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
            if (gameObject.tag == "Player1")
            {
                player1NoDropOrbZone = false;
            }

            if (gameObject.tag == "Player2")
            {
                player2NoDropOrbZone = false;
            }

            if (gameObject.tag == "Player3")
            {
                player3NoDropOrbZone = false;
            }

            if (gameObject.tag == "Player4")
            {
                player4NoDropOrbZone = false;
            }

        }
        if (other.tag == "Room" || other.tag == "MurdererStart")
        {
            lastRoom = currentRoom;
            currentRoom = other.gameObject;
            moveCamera = true;
        }
        if (other.tag == "BluePotion")
        {
            playerSpeed *= 2;
            speedIncreased = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "RedPotion")
        {
            // Teleport to another room
            while(teleported ==false){
                int x = Random.Range(1, 9);
                if (x == 1 && TopLeftRoomLocation.x >= -2 && TopLeftRoomLocation.y <= 2)
                {
                    this.transform.position = TopLeftRoom.transform.position;
                    teleported = true;
                    Debug.Log("Topleft: "+TopLeftRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 2 && TopMiddleRoomLocation.y <= 2)
                {
                    this.transform.position = TopMiddleRoom.transform.position;
                    teleported = true;
                    Debug.Log("Topmiddle: " + TopMiddleRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 3 && TopRightRoomLocation.x <= 2 && TopRightRoomLocation.y <= 2)
                {
                    this.transform.position = TopRightRoom.transform.position;
                    teleported = true;
                    Debug.Log("Topright: " + TopRightRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 4 && LeftRoomLocation.x >= -2)
                {
                    this.transform.position = LeftRoom.transform.position;
                    teleported = true;
                    Debug.Log("left: " + LeftRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 5 && RightRoomLocation.x <= 2)
                {
                    this.transform.position = RightRoom.transform.position;
                    teleported = true;
                    Debug.Log("right: " + RightRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 6 && BottomLeftRoomLocation.x >= -2 && BottomLeftRoomLocation.y >= -2)
                {
                    this.transform.position = BottomLeftRoom.transform.position;
                    teleported = true;
                    Debug.Log("bottomleft: " + BottomLeftRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 7 && BottomMiddleRoomLocation.y >= -2)
                {
                    this.transform.position = BottomMiddleRoom.transform.position;
                    teleported = true;
                    Debug.Log("bottommiddle: " + BottomMiddleRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 8 && BottomRightRoomLocation.x <= 2 && BottomRightRoomLocation.y >= -2)
                {
                    this.transform.position = BottomRightRoom.transform.position;
                    teleported = true;
                    Debug.Log("bottomRight: " + BottomRightRoomLocation);
                    MoveCameraOnTeleport();
                }
            }
        
            teleported = false;
            Destroy(other.gameObject);
        }
        //set bools to start panning
        if (other.gameObject.tag == "OptionsRoom")
        {
            moveCameraLeft = true;
            moveCameraToCenter = false;
        }
        if (other.gameObject.tag == "PlayerCustomizationRoom")
        {
            moveCameraRight = true;
            moveCameraToCenter = false;
        }

        //kill player and turn on blood on murderer's shirt
        if (other.gameObject.tag == "Knife1" || other.gameObject.tag == "Knife2" || other.gameObject.tag == "Knife3" || other.gameObject.tag == "Knife4")
        {
            if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" && gameObject.tag != "Murderer4" && 
                gameObject.tag != "DownedWimp1" && gameObject.tag != "DownedWimp2" && gameObject.tag != "DownedWimp3" && gameObject.tag != "DownedWimp4")
            {
                if (diseased) {
                    MurdererScripts.diseased = true;
                }

                if (MurdererScripts.thrillActive)
                {
                    MurdererScripts.thrill = true;
                }

                TorsoAnimator.SetBool("Dead", true);
                LegsAnimator.SetBool("Dead", true);
                gameObject.layer = 8;
                dead = true;
                wimpKilled = true;
                //increment wimps down needed for murderer win condition
                if(Time.time>wimpDownedCooldown + wimpDownedDelay)
                {
                    wimpsDowned += 1;
                    wimpDownedCooldown = Time.time;
                }
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                groundCheck.GetComponent<Collider2D>().isTrigger = true;
                switch (gameObject.tag)
                {
                    case "Player1":
                        gameObject.tag = "DownedWimp1";
                        break;
                    case "Player2":
                        gameObject.tag = "DownedWimp2";
                        break;
                    case "Player3":
                        gameObject.tag = "DownedWimp3";
                        break;
                    case "Player4":
                        gameObject.tag = "DownedWimp4";
                        break;
                }
                
                Invoke("TurnOnBlood", .5f);
                //murderer weapon cooldown after killing wimp
                Invoke("RechargeWeapon", MurdererWeaponCooldown);
                
                bloodStained = true;
            }
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "BotLadder")
        {
            canGoDown = false;
            canGoUp = false;
        }
        if (other.tag == "TopLadder")
        {
            canGoDown = false;
            canGoUp = false;
        }
        if (other.tag == "ColorSelection")
        {
            GameObject.Find("ColorManager").GetComponent<ColorManager>().playerCanSelect = false;
        }
        if (other.tag == "Room" || other.tag == "MurdererStart")
        {
            lastRoom = other.gameObject;
        }
        //set bools to start panning
        if (other.gameObject.tag == "OptionsRoom")
        {
            moveCameraLeft = false;
            moveCameraToCenter = true;
        }
        if (other.gameObject.tag == "PlayerCustomizationRoom")
        {
            moveCameraRight = false;
            moveCameraToCenter = true;
        }
        //only works on main menu scene
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            GateHighlight.enabled = false;
        }
    }

    //functions used to turn on bloody shirt visibility for all cameras
    void TurnOnBlood()
    {
        GameObject.FindGameObjectWithTag("Player1Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
        GameObject.FindGameObjectWithTag("Player2Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
        GameObject.FindGameObjectWithTag("Player3Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
        GameObject.FindGameObjectWithTag("Player4Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
    }

    //function used for murderer weapon cooldown
    void RechargeWeapon()
    {
        wimpKilled = false;
    }

    void GetTeleportLocations() {
        TopLeftRoomLocation = currentPos + new Vector2(-1, 1);
        TopMiddleRoomLocation = currentPos + new Vector2(0, 1);
        TopRightRoomLocation = currentPos + new Vector2(1, 1);
        LeftRoomLocation = currentPos + new Vector2(-1, 0);
        RightRoomLocation = currentPos + new Vector2(1, 0);
        BottomLeftRoomLocation = currentPos + new Vector2(-1, -1);
        BottomMiddleRoomLocation = currentPos + new Vector2(0, -1);
        BottomRightRoomLocation = currentPos + new Vector2(1, -1);
    }

    void MoveCameraOnTeleport() {
        if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1")
        {
            player1Camera.transform.position = this.transform.position;
        }

        if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
        {
            player2Camera.transform.position = this.transform.position;
        }

        if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
        {
            player3Camera.transform.position = this.transform.position;
        }

        if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
        {
            player4Camera.transform.position = this.transform.position;
        }
    }
    
}
