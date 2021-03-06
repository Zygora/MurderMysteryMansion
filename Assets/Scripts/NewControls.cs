﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class NewControls : MonoBehaviour {

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
    public GameObject currentRoom;
    public GameObject lastRoom;
    public static GameObject TopLeftRoom;
    public static GameObject TopMiddleRoom;
    public static GameObject TopRightRoom;
    public static GameObject LeftRoom;
    public static GameObject RightRoom;
    public static GameObject BottomLeftRoom;
    public static GameObject BottomMiddleRoom;
    public static GameObject BottomRightRoom;
    private GameObject ladder;
    private GameObject[] Shirts;

    public Animator TorsoAnimator;
    public Animator LegsAnimator;
    public Animator ShirtAnimator;

    public AudioSource Audio;
    public AudioClip scream;
    public AudioClip trap;

    public bool moveCameraLeft;
    public bool moveCameraRight;
    public bool moveCameraToCenter;
    public static bool player1NoDropOrbZone = false;
    public static bool player2NoDropOrbZone = false;
    public static bool player3NoDropOrbZone = false;
    public static bool player4NoDropOrbZone = false;
    public static bool Trapped = false;
    public static bool murderTransitioning = false;
    public static bool bloodStained = false;
    public static bool wimpKilled = false;
    public bool canMove = true;
    public bool diseased;
    public bool drNerd;
    public bool crazedAlchemist;
    public bool scaredCat;
    public bool Trapper = false;
    public bool goUp;
    public bool goDown;
    public bool exited;
    public bool moveCamera;
    bool speedIncreased;
    private bool currentPlayerTrapped;
    private bool teleported = false;
    private bool dead;
    private bool canDie;

    float revivetime = 2.0f;
    float revivetimepassed = 0.0f;
    float speedMultiplier;
    float timeSpeedIncreased;
    public float thrillSpeedBoost;
    public float cameraSpeed = 30;
    public float direction = 1;
    public float MenuDownHoldTime;
    public float playerTransitionSpeed;
    public static float trappedTime;
    private float timeSinceTrapped;
    private float gravityScale;
    private float wimpDownedCooldown;
    private float wimpDownedDelay = 1;

    public string horizontal;
    public string vertical;
    public string revive1;
    public string revive2;
    public string revive3;
    public string revive4;
    public string jump;
    public string revive;
    private string interact;

    public static int wimpsDowned = 0;
    public int MurdererWeaponCooldown;

    public static Vector2 TopLeftRoomLocation;
    public static Vector2 TopMiddleRoomLocation;
    public static Vector2 TopRightRoomLocation;
    public static Vector2 LeftRoomLocation;
    public static Vector2 RightRoomLocation;
    public static Vector2 BottomLeftRoomLocation;
    public static Vector2 BottomMiddleRoomLocation;
    public static Vector2 BottomRightRoomLocation;
    public static Vector2 currentPlayer1Pos;
    public static Vector2 currentPlayer2Pos;
    public static Vector2 currentPlayer3Pos;
    public static Vector2 currentPlayer4Pos;
    public static Vector2 murdererPosition;
    public Vector2 currentPos;
    public static List<GameObject> camList;

    private static System.Random rng = new System.Random();

    public void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1) 
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    float xVal(int a)
    {
        switch (a)
        {

            case 0: return 0.0f;
            case 1: return 0.5f;
            case 2: return 0.0f;
            case 3: return 0.5f;
            default: return 0.0f;
        }

    }
    float yVal(int a)
    {
        switch (a)
        {

            case 0: return 0.0f;
            case 1: return 0.0f;
            case 2: return 0.5f;
            case 3: return 0.5f;
            default: return 0.0f;
        }

    }

    void Start()
    {
        // Save players gravity scale
        gravityScale = gameObject.GetComponent<Rigidbody2D>().gravityScale;
        // Get rigidbody of the player at start
        rb = GetComponent<Rigidbody2D>();
        // Time that player should hold down button near the gates in order to exit the game
        MenuDownHoldTime = 0;
        // Spped of the player when he runs from screen to screen while transitioning
        playerTransitionSpeed = playerSpeed / 4;
        dead = false;
        //grab cameras in scene to use for split screen transitions
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

        //Randomize camera placements
        int val = Random.Range(0, 4);
        int c1 = 1, c2 = 2, c3 = 3, c4 = 4;

        List<int> arrayList = new List<int>();

        arrayList.Add(c1);
        arrayList.Add(c2);
        arrayList.Add(c3);
        arrayList.Add(c4);

        Shuffle(arrayList);

        // Debug.Log("List:"+cList[1]);

        player1Camera.GetComponent<Camera>().rect = new Rect(xVal(arrayList[0]), yVal(arrayList[0]), 0.5f, 0.5f);
        player2Camera.GetComponent<Camera>().rect = new Rect(xVal(arrayList[1]), yVal(arrayList[1]), 0.5f, 0.5f);
        player3Camera.GetComponent<Camera>().rect = new Rect(xVal(arrayList[2]), yVal(arrayList[2]), 0.5f, 0.5f);
        player4Camera.GetComponent<Camera>().rect = new Rect(xVal(arrayList[3]), yVal(arrayList[3]), 0.5f, 0.5f);

        //reset values
        Trapped = false;
        bloodStained = false;
        wimpKilled = false;

    }

    void Update()
    {
        
        //track the player being trapped
        if (Trapped == false && currentPlayerTrapped == false)
        {
            timeSinceTrapped = 0;
        }

        if (Trapped == true && currentPlayerTrapped == true)
        {
            TorsoAnimator.Play("TorsoDizzy");
            LegsAnimator.Play("LegsDeath");
            TorsoAnimator.SetBool("Dizzy", true);
            timeSinceTrapped += Time.deltaTime;
            if (timeSinceTrapped >= trappedTime)
            {
                if (speedIncreased == false)
                {
                    playerSpeed = speedWhileNotCarryOrb;
                }

                if (speedIncreased == true)
                {
                    playerSpeed = playerSpeed * 2;
                }

                timeSinceTrapped = 0;
                Trapped = false;
                TorsoAnimator.SetBool("Dizzy", false);
                currentPlayerTrapped = false;
            }
        }

        //track positions of player and get teleport locations if player is the crazed alchemist

        if (gameObject.tag == "Player1")
        {
            currentPos = currentPlayer1Pos;
            if (crazedAlchemist == true)
            {
                GetTeleportLocations();
            }
        }

        if (gameObject.tag == "Player2")
        {
            currentPos = currentPlayer2Pos;
            if (crazedAlchemist == true)
            {
                GetTeleportLocations();
            }
        }

        if (gameObject.tag == "Player3")
        {
            currentPos = currentPlayer3Pos;
            if (crazedAlchemist == true)
            {
                GetTeleportLocations();
            }
        }

        if (gameObject.tag == "Player4")
        {
            currentPos = currentPlayer4Pos;
            if (crazedAlchemist == true)
            {
                GetTeleportLocations();
            }
        }

        //turn off movement if the game is over
        if (GameOverTextManager.gameOver == true || dead == true)
        {
            playerSpeed = 0;
        }

        if (GameOverTextManager.gameOver == false)
        {
            //reset dead bool and animator bools when player is revived/alive
            if (gameObject.tag == "Player1" || gameObject.tag == "Player2" || gameObject.tag == "Player3" || gameObject.tag == "Player4")
            {
                dead = false;
                TorsoAnimator.SetBool("Dead", false);
                LegsAnimator.SetBool("Dead", false);
            }

            //disable sprite renderer if wimp has exited
            if (gameObject.tag == "Player1" && OrbCount.player1Exited == true)
            {
                OnWimpExit();
            }

            if (gameObject.tag == "Player2" && OrbCount.player2Exited == true)
            {
                OnWimpExit();
            }

            if (gameObject.tag == "Player3" && OrbCount.player3Exited == true)
            {
                OnWimpExit();
            }

            if (gameObject.tag == "Player4" && OrbCount.player4Exited == true)
            {
                OnWimpExit();
            }
            //enable sprite renderer if wimp has re entered
            if (gameObject.tag == "Player1" && OrbCount.player1Exited == true && Input.GetButtonDown(interact) && OrbCount.player1CanEnter == true)
            {
                OnWimpReEnter();
                OrbCount.player1Exited = false;
                OrbCount.player1CanEnter = false;
            }

            if (gameObject.tag == "Player2" && OrbCount.player2Exited == true && Input.GetButtonDown(interact) && OrbCount.player2CanEnter == true)
            {
                OnWimpReEnter();
                OrbCount.player2Exited = false;
                OrbCount.player2CanEnter = false;

            }

            if (gameObject.tag == "Player3" && OrbCount.player3Exited == true && Input.GetButtonDown(interact) && OrbCount.player3CanEnter == true)
            {
                OnWimpReEnter();
                OrbCount.player3Exited = false;
                OrbCount.player3CanEnter = false;

            }

            if (gameObject.tag == "Player4" && OrbCount.player4Exited == true && Input.GetButtonDown(interact) && OrbCount.player4CanEnter == true)
            {
                OnWimpReEnter();
                OrbCount.player4Exited = false;
                OrbCount.player4CanEnter = false;

            }

            //decrease speed of player if carrying an orb
            if (gameObject.tag == "Player1" && OrbCount.player1CarryOrb == true && currentPlayerTrapped == false && dead == false)
            {
                DecreaseSpeedCarryingOrb();
            }

            if (gameObject.tag == "Player2" && OrbCount.player2CarryOrb == true && currentPlayerTrapped == false && dead == false)
            {
                DecreaseSpeedCarryingOrb();
            }

            if (gameObject.tag == "Player3" && OrbCount.player3CarryOrb == true && currentPlayerTrapped == false && dead == false)
            {
                DecreaseSpeedCarryingOrb();
            }

            if (gameObject.tag == "Player4" && OrbCount.player4CarryOrb == true && currentPlayerTrapped == false && dead == false)
            {
                DecreaseSpeedCarryingOrb();
            }

            //restore speed of player if not carrying an orb
            if (gameObject.tag == "Player1" && OrbCount.player1CarryOrb == false && currentPlayerTrapped == false && dead == false)
            {
                RestoreSpeedNotCarryingOrb();
            }

            if (gameObject.tag == "Player2" && OrbCount.player2CarryOrb == false && currentPlayerTrapped == false && dead == false)
            {
                RestoreSpeedNotCarryingOrb();
            }

            if (gameObject.tag == "Player3" && OrbCount.player3CarryOrb == false && currentPlayerTrapped == false && dead == false)
            {
                RestoreSpeedNotCarryingOrb();
            }

            if (gameObject.tag == "Player4" && OrbCount.player4CarryOrb == false && currentPlayerTrapped == false && dead == false)
            {
                RestoreSpeedNotCarryingOrb();
            }
        }

        //keep track of speed increase from getting hit by a blue potion
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


        if (dead == false && exited == false)
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

                    if (MurdererScripts.thrill == true && ShittyPossum.possumed == false && MurdererScripts.washingClothes == false && MurdererScripts.diseased == false)
                    {
                        playerSpeed = thrillSpeedBoost + speedWhileNotCarryOrb;
                    }

                    //restore movement of murder if above conditions dont apply after
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
                    //allow use of abilities while on ground
                    SetNoAbilityOrOrbUseZone(false);
                }
                // otherwise the player is in the air
                else if (hit.distance >= 12f)//1.3
                {
                    onGround = false;
                    TorsoAnimator.SetBool("Jumping", true);
                    LegsAnimator.SetBool("Jumping", true);
                    //disable use of abilities while on ground
                    SetNoAbilityOrOrbUseZone(true);
                }
            }
            // Create move vector
            if (goUp || goDown)
            {
                move = new Vector3(0, Input.GetAxis(vertical), 0);
            }

            else
                move = new Vector3(Input.GetAxis(horizontal), 0, 0);

            //set movement bounds on player
            if (transform.position.x <= -95)
            {
                transform.position = new Vector3(-95, transform.position.y, transform.position.z);
            }

            if (transform.position.x >= 815)
            {
                transform.position = new Vector3(815, transform.position.y, transform.position.z);
            }

            if (transform.position.y <= -447.5f)
            {
                transform.position = new Vector3(transform.position.x, -447.5f, transform.position.z);
            }

            if (transform.position.y >= 447.5f)
            {
                transform.position = new Vector3(transform.position.x, 447.5f, transform.position.z);
            }

            // Move the player
            transform.position += move * playerSpeed * Time.deltaTime;

            //murderer animations
            if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
            {

                if (murderTransitioning != true && MurdererScripts.diseased == false)
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
                    if (Input.GetAxis(horizontal) < -0.1f && diseased == false)
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
                    if (Input.GetAxis(horizontal) > 0.1f && diseased == false)
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

                else
                {
                    ShirtAnimator.SetBool("MurdererIdle", false);
                    ShirtAnimator.SetBool("MurdererJumping", false);
                }
            }

            //player animations
            if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3"
                 && gameObject.tag != "Murderer4" && currentPlayerTrapped == false)
            {
                    PlayerAnimations();
            }

            // If player is on ground and space button hit -> jump
            if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" &&
            gameObject.tag != "Murderer4")
            {
                if (Input.GetButtonDown(jump) && (onGround) && currentPlayerTrapped == false && dead == false)
                {
                    rb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                }
            }

            //if murderer is on the ground and not diseased or washing clothes murderer can jump
            if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" ||
            gameObject.tag == "Murderer4")
            {
                if (Input.GetButtonDown(jump) && (onGround) && MurdererScripts.diseased == false
                    && MurdererScripts.washingClothes == false)
                {
                    rb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                }
            }
        }

        //go to main menu
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        //reset current level
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(4); // load level generator
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
        if (Input.GetAxis(vertical) > 0 && (canGoUp) && (!gameObject.tag.Contains("DownedWimp")))
        {
            goUp = true;
            gameObject.transform.position = new Vector3(ladder.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }

        // Go up
        if ((goUp) && (!gameObject.tag.Contains("DownedWimp")))
        {
            canMove = false;
            groundCheck.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        // if player hits downArrow on the ladder go up
        if (Input.GetAxis(vertical) < 0 && (canGoDown) && (!gameObject.tag.Contains("DownedWimp")))
        {
            goDown = true;
            gameObject.transform.position = new Vector3(ladder.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        // Go down
        if ((goDown) && (!gameObject.tag.Contains("DownedWimp")))
        {
            canMove = false;
            groundCheck.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        // Change player animation to jump while transitioning up or down
        if (goDown || goUp)
        {
            TorsoAnimator.SetBool("Jumping", true);
            LegsAnimator.SetBool("Jumping", true);
            SetNoAbilityOrOrbUseZone(true);
            SetKillZone(true);
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
           
            // Change player animation to run while transitioning left or right
            if ((!goUp) && (!goDown))
            {
                SetNoAbilityOrOrbUseZone(true);
                SetKillZone(true);
            }
            if ((goDown) && (Input.GetAxis(vertical) > 0))
            {
                direction *= -1;
                canMove = false;
                goDown = false;
                goUp = true;
            }
            if ((goUp) && (Input.GetAxis(vertical) < 0))
            {
                direction *= -1;
                canMove = false;
                goDown = true;
                goUp = false;
            }
            if (Input.GetAxis(horizontal) < 0 && (direction.x == 1) && (!goDown) && (!goUp))
            {
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
  
                    direction *= -1;
                    canMove = false;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    if (speedMultiplier <= 2)
                    {
                        speedMultiplier = 2;
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
            if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1")
            {
                currentPlayer1Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                if (gameObject.tag == "Murderer1")
                {
                    murdererPosition = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                }
            }

            if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
            {
                currentPlayer2Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                if (gameObject.tag == "Murderer2")
                {
                    murdererPosition = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                }
            }

            if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
            {
                currentPlayer3Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                if (gameObject.tag == "Murderer3")
                {
                    murdererPosition = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                }
            }

            if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
            {
                currentPlayer4Pos = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                if (gameObject.tag == "Murderer4")
                {
                    murdererPosition = other.gameObject.GetComponent<AddVectorToRoom>().roomCoordinate;
                }
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

        //collision interaction with trap
        if (other.gameObject.tag == "Trap" && dead == false)
        {
            if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" &&
                gameObject.tag != "Murderer4")
            {
                Trapped = true;
                currentPlayerTrapped = true;
                Audio.PlayOneShot(trap);
                playerSpeed = 0;
                Destroy(other.gameObject, 2);
                TorsoAnimator.SetBool("Running", false);
                LegsAnimator.SetBool("Running", false);
                TorsoAnimator.SetBool("Idle", true);
                LegsAnimator.SetBool("Idle", true);
            }
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
            SetNoAbilityOrOrbUseZone(false);
            SetKillZone(false);
            if (scaredCat && ScaredCat.scaredCatRunning)
            {
                ladder = other.gameObject;
                canGoUp = true;
                if (ScaredCat.scaredCatOnLadder == true)
                {
                    ScaredCat.scaredCatContinueRunning = true;
                    canGoDown = false;
                    canGoUp = false;
                }
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
            SetNoAbilityOrOrbUseZone(false);
            SetKillZone(false);
            if (scaredCat && ScaredCat.scaredCatRunning)
            {
                ladder = other.gameObject;
                canGoDown = true;
                if (ScaredCat.scaredCatOnLadder == true)
                {
                    ScaredCat.scaredCatContinueRunning = true;
                    canGoDown = false;
                    canGoUp = false;
                }
            }
        }

        if (other.tag == "Room" || other.tag == "MurdererStart")
        {
            lastRoom = currentRoom;
            currentRoom = other.gameObject;
            moveCamera = true;
        }
        //interaction with blue potion of crazed alchemist
        if (other.tag == "BluePotion" && dead == false)
        {
            playerSpeed *= 2;
            speedIncreased = true;
            Destroy(other.gameObject);
        }
        //interaction with red potion of crazed alchemist
        if (other.tag == "RedPotion" && dead == false)
        {
            // Teleport to another room
            while (teleported == false)
            {
                int x = Random.Range(1, 9);
                //teleport to random adjacent room of the crazed alchemist
                if (x == 1 && TopLeftRoomLocation.x >= 0 && TopLeftRoomLocation.y <= 3)
                {
                    this.transform.position = TopLeftRoom.transform.position;
                    Debug.Log("Topleft: " + TopLeftRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 2 && TopMiddleRoomLocation.y <= 3)
                {
                    this.transform.position = TopMiddleRoom.transform.position;
                    Debug.Log("Topmiddle: " + TopMiddleRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 3 && TopRightRoomLocation.x <= 3 && TopRightRoomLocation.y <= 3)
                {
                    this.transform.position = TopRightRoom.transform.position;
                    Debug.Log("Topright: " + TopRightRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 4 && LeftRoomLocation.x >= 0)
                {
                    this.transform.position = LeftRoom.transform.position;
                    Debug.Log("left: " + LeftRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 5 && RightRoomLocation.x <= 3)
                {
                    this.transform.position = RightRoom.transform.position;
                    Debug.Log("right: " + RightRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 6 && BottomLeftRoomLocation.x >= 0 && BottomLeftRoomLocation.y >= 0)
                {
                    this.transform.position = BottomLeftRoom.transform.position;
                    Debug.Log("bottomleft: " + BottomLeftRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 7 && BottomMiddleRoomLocation.y >= 0)
                {
                    this.transform.position = BottomMiddleRoom.transform.position;
                    Debug.Log("bottommiddle: " + BottomMiddleRoomLocation);
                    MoveCameraOnTeleport();
                }

                if (x == 8 && BottomRightRoomLocation.x <= 3 && BottomRightRoomLocation.y >= 0)
                {
                    this.transform.position = BottomRightRoom.transform.position;
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
            if (canMove)
            {
                if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" && gameObject.tag != "Murderer4" &&
                    gameObject.tag != "DownedWimp1" && gameObject.tag != "DownedWimp2" && gameObject.tag != "DownedWimp3" && gameObject.tag != "DownedWimp4"
                    && canDie == false)
                {
                    //activate diseased if killed player was diseased
                    if (diseased)
                    {
                        MurdererScripts.diseased = true;
                    }

                    //activate thrill of the hunt if the murderer has the ability
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
                    if (Time.time > wimpDownedCooldown + wimpDownedDelay)
                    {
                        wimpsDowned += 1;
                        wimpDownedCooldown = Time.time;
                    }
                    gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    groundCheck.GetComponent<Collider2D>().isTrigger = true;
                    //change player tags for use with the revive mechanic
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

    //FUNCTIONS
    //turn on bloody shirt visibility for all cameras
    void TurnOnBlood()
    {
        GameObject.FindGameObjectWithTag("Player1Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
        GameObject.FindGameObjectWithTag("Player2Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
        GameObject.FindGameObjectWithTag("Player3Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
        GameObject.FindGameObjectWithTag("Player4Camera").GetComponent<Camera>().cullingMask |= (1 << 16);
    }

    //murderer weapon cooldown
    void RechargeWeapon()
    {
        wimpKilled = false;
    }

    //grap teleport vector locations for crazed alchemist red potion
    //values then passed to room gameobjects in order to get position of room
    void GetTeleportLocations()
    {
        TopLeftRoomLocation = currentPos + new Vector2(-1, 1);
        TopMiddleRoomLocation = currentPos + new Vector2(0, 1);
        TopRightRoomLocation = currentPos + new Vector2(1, 1);
        LeftRoomLocation = currentPos + new Vector2(-1, 0);
        RightRoomLocation = currentPos + new Vector2(1, 0);
        BottomLeftRoomLocation = currentPos + new Vector2(-1, -1);
        BottomMiddleRoomLocation = currentPos + new Vector2(0, -1);
        BottomRightRoomLocation = currentPos + new Vector2(1, -1);
    }

    //move camera instantaneously when the player is teleported by the red potion
    void MoveCameraOnTeleport()
    {
        if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1")
        {
            player1Camera.transform.position = this.transform.position;
            teleported = true;
        }

        if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
        {
            player2Camera.transform.position = this.transform.position;
            teleported = true;
        }

        if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
        {
            player3Camera.transform.position = this.transform.position;
            teleported = true;
        }

        if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
        {
            player4Camera.transform.position = this.transform.position;
            teleported = true;
        }
    }

    //decrease speed of player while carrying an orb
    //additional conditions to make the speed decrease interact with other speed increases/decrease in the game
    void DecreaseSpeedCarryingOrb()
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

    //restore speed of player while carrying an orb
    //additional conditions to make the speed restoraion interact with other speed increases/decrease in the game
    void RestoreSpeedNotCarryingOrb()
    {
        if (speedIncreased == false)
        {
            playerSpeed = speedWhileNotCarryOrb;
        }
    }

    void PlayerAnimations()
    {
        //play idle animations
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

    //simulate wimp leaving the level
    void OnWimpExit()
    {
        //make exited player not visible to the camera
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -50);
        playerSpeed = 0;
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        groundCheck.GetComponent<Collider2D>().enabled = false;
        exited = true;
    }

    //simulate wimp re entering the level
    void OnWimpReEnter()
    {
        //make player visible to camera again
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        playerSpeed = speedWhileNotCarryOrb;
        TorsoAnimator.SetBool("Running", false);
        LegsAnimator.SetBool("Running", false);
        TorsoAnimator.SetBool("Idle", true);
        LegsAnimator.SetBool("Idle", true);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        gameObject.GetComponent<Collider2D>().enabled = true;
        groundCheck.GetComponent<Collider2D>().enabled = true;
        OrbCount.wimpsExited -= 1;
        exited = false;
    }

    //set zones where abilities cant be used and orb cant be dropped to false
    void SetNoAbilityOrOrbUseZone(bool inZone)
    {
        if (gameObject.tag == "Player1" || gameObject.tag == "Murderer1")
        {
            player1NoDropOrbZone = inZone;
        }

        if (gameObject.tag == "Player2" || gameObject.tag == "Murderer2")
        {
            player2NoDropOrbZone = inZone;
        }

        if (gameObject.tag == "Player3" || gameObject.tag == "Murderer3")
        {
            player3NoDropOrbZone = inZone;
        }

        if (gameObject.tag == "Player4" || gameObject.tag == "Murderer4")
        {
            player4NoDropOrbZone = inZone;
        }
    }

    //set areas where player can and can't be killed
    void SetKillZone(bool canBeKilled)
    {
        canDie = canBeKilled;
    }
}
