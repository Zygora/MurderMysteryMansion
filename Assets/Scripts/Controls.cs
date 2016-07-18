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
    public string revive;

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
    public static bool murderTransitioning= false;
    public static bool bloodStained = false;
    public int speedWhileCarryOrb;
    public static bool wimpKilled = false;

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
    }

    void Update()
    {
        //decrease speed of player if carrying an orb
        if (gameObject.tag == "Player1" && OrbCount.player1CarryOrb == true)
        {
            playerSpeed = speedWhileCarryOrb;
        }

        if (gameObject.tag == "Player2" && OrbCount.player2CarryOrb == true)
        {
            playerSpeed = speedWhileCarryOrb;
        }

        if (gameObject.tag == "Player3" && OrbCount.player3CarryOrb == true)
        {
            playerSpeed = speedWhileCarryOrb;
        }

        if (gameObject.tag == "Player4" && OrbCount.player4CarryOrb == true)
        {
            playerSpeed = speedWhileCarryOrb;
        }

        //restore speed of player if not carrying an orb
        if (gameObject.tag == "Player1" && OrbCount.player1CarryOrb == false)
        {
            playerSpeed = 40;
        }

        if (gameObject.tag == "Player2" && OrbCount.player2CarryOrb == false)
        {
            playerSpeed = 40;
        }

        if (gameObject.tag == "Player3" && OrbCount.player3CarryOrb == false)
        {
            playerSpeed = 40;
        }

        if (gameObject.tag == "Player4" && OrbCount.player4CarryOrb == false)
        {
            playerSpeed = 40;
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

        if (canMove && dead == false )
        {
            // Create a ray down checking if there is anything underneath the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
            // Debug.DrawRay(transform.position, Vector2.down, Color.red);
            // If ray hit something player is on ground
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Ground" && hit.distance < 1.3f)
                {
                    onGround = true;
                    //Animator.SetBool("InAir", false);
                    TorsoAnimator.SetBool("Jumping", false);
                    LegsAnimator.SetBool("Jumping", false);
                }
                // otherwise the player is in the air
                else if (hit.distance >= 1.3f)
                {
                    // Debug.Log(hit.distance);
                    onGround = false;
                    //Animator.SetBool("InAir", true);
                    TorsoAnimator.SetBool("Jumping", true);
                    LegsAnimator.SetBool("Jumping", true);
                }
            }
            // Create move vector
            move = new Vector3(Input.GetAxis(horizontal), 0, 0);
            //set movement bounds on player
            if (transform.position.x <= -110) {
                transform.position = new Vector3(-105,transform.position.y,transform.position.z);
            }

            if (transform.position.x >= 1070)
            {
                transform.position = new Vector3(1065, transform.position.y, transform.position.z);
            }

            if (transform.position.y <= -363.5f)
            {
                transform.position = new Vector3(transform.position.x,-343.5f, transform.position.z);
            }

            if (transform.position.y >= 311.5f)
            {
                transform.position = new Vector3(transform.position.x, 291.5f, transform.position.z);
            }

            // Move the player
            transform.position += move * playerSpeed * Time.deltaTime;

            //disable movement for murderer if murderer is washing clothes
            if (MurdererScripts.washingClothes == true) {
                if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4") {
                    playerSpeed = 0;
                    playerJumpForce = 0;
                }
            }

            //enable movement for murderer if murderer is not washing clothes
            if (MurdererScripts.washingClothes == false)
            {
                if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                {
                    playerSpeed = 40;
                    playerJumpForce = 75;
                }
            }

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
                TorsoAnimator.SetBool("Running", true);
                LegsAnimator.SetBool("Running", true);
                TorsoAnimator.SetBool("Idle", false);
                LegsAnimator.SetBool("Idle", false);
                //GetComponent<SpriteRenderer>().flipX = true;
                transform.eulerAngles = new Vector3(0, 180, 0);
                direction = -1;
            }
            // Change animation from idle to run and flip the sprite
            if (Input.GetAxis(horizontal) > 0.1f)
            {
                TorsoAnimator.SetBool("Running", true);
                LegsAnimator.SetBool("Running", true);
                TorsoAnimator.SetBool("Idle", false);
                LegsAnimator.SetBool("Idle", false);
                // GetComponent<SpriteRenderer>().flipX = false;
                transform.eulerAngles = new Vector3(0, 0, 0);
                direction = 1;
            }
            // If player is on ground and space button hit -> jump
            if (Input.GetButtonDown(jump) && (onGround))
            {
                rb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
               // Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), topPlatform.GetComponent<Collider2D>(), false);
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
        /*Test death animation
        if (Input.GetKey(KeyCode.Q))
        {
            TorsoAnimator.SetBool("Dead", true);
            LegsAnimator.SetBool("Dead", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            TorsoAnimator.SetBool("Dead", false);
            LegsAnimator.SetBool("Dead", false);
        }*/

        // if player hits upArrow on the ladder go up
        if(Input.GetAxis(vertical) > 0 &&(canGoUp))
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
            transform.position += new Vector3(0,1,0) * playerClimbSpeed * Time.deltaTime * speedMultiplier;
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
            if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4") {
                ShirtAnimator.SetBool("MurdererJumping", true);
                murderTransitioning = true;
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
            if ((!goUp)&&(!goDown))
            {
                TorsoAnimator.SetBool("Running", true);
                LegsAnimator.SetBool("Running", true);
                TorsoAnimator.SetBool("Idle", false);
                LegsAnimator.SetBool("Idle", false);
                if (gameObject.tag == "Murderer1" || gameObject.tag == "Murderer2" || gameObject.tag == "Murderer3" || gameObject.tag == "Murderer4")
                {
                    ShirtAnimator.SetBool("MurdererRunning", true);
                    ShirtAnimator.SetBool("MurdererIdle", false);
                    murderTransitioning = true;
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
                if(speedMultiplier <=2)
                {
                    speedMultiplier = 2;
                }
            }
            if (Input.GetAxis(horizontal) > 0 && (direction.x == -1)&&(!goDown)&&(!goUp))
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
                    murderTransitioning = false;
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
                    murderTransitioning = false;
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
                    murderTransitioning = false;
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
                    murderTransitioning = false;
                }
            }


          /*  if (direction.x == 1)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (direction.x == -1)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            */
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
            murderTransitioning = false;
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

        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "Player3" || other.gameObject.tag == "Player4")
        {

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
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            groundCheck.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
        }
        if (other.tag == "TopLadder")
        {
            canMove = true;
            goUp = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
           // gameObject.GetComponent<Collider2D>().enabled = true;
            groundCheck.GetComponent<Collider2D>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = gravityScale;
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
            if (gameObject.tag != "Murderer1" && gameObject.tag != "Murderer2" && gameObject.tag != "Murderer3" && gameObject.tag != "Murderer4")
            {
                TorsoAnimator.SetBool("Dead", true);
                LegsAnimator.SetBool("Dead", true);
                gameObject.layer = 8;
                dead = true;
                wimpKilled = true;
                gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                gameObject.GetComponent<Collider2D>().isTrigger = true;
                // if murderer has thrill of the hunt script atached activate the bonus
                if((other.gameObject.GetComponent<ThrillOfTheHunt>() as ThrillOfTheHunt)!=null)
                {
                    other.gameObject.GetComponent<ThrillOfTheHunt>().ActivateBonus();
                }
                switch(gameObject.tag)
                {
                    case "Player1": gameObject.tag = "DownedWimp1";
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
               // transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 5, this.transform.position.z);
                if (other.gameObject.tag == "Knife1") {
                    Invoke("TurnOnBloodShirt1", .5f);
                }

                if (other.gameObject.tag == "Knife2")
                {
                    Invoke("TurnOnBloodShirt2", .5f);
                }

                if (other.gameObject.tag == "Knife3")
                {
                    Invoke("TurnOnBloodShirt3", .5f);
                }

                if (other.gameObject.tag == "Knife4")
                {
                    Invoke("TurnOnBloodShirt4", .5f);
                }
                Invoke("RechargeWeapon", 5);
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

    void TurnOnBloodShirt1() {
        GameObject.FindGameObjectWithTag("MurdererShirt1").GetComponent<Animator>().enabled = true;
    }

    void TurnOnBloodShirt2()
    {
        GameObject.FindGameObjectWithTag("MurdererShirt2").GetComponent<Animator>().enabled = true;
    }

    void TurnOnBloodShirt3()
    {
        GameObject.FindGameObjectWithTag("MurdererShirt3").GetComponent<Animator>().enabled = true;
    }

    void TurnOnBloodShirt4()
    {
        GameObject.FindGameObjectWithTag("MurdererShirt4").GetComponent<Animator>().enabled = true;
    }

    void RechargeWeapon() {
        wimpKilled = false;
    }
}
