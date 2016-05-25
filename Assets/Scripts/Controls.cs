using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Controls : MonoBehaviour {
    // Tweakable variables
    [Header("Tweakable variables")]
    [Range(1, 15)]
    public float playerSpeed;
    [Range(20, 70)]
    public float playerJumpForce;
    [Range(1, 15)]
    public float playerClimbSpeed;
    [Space(10), ]
    // System variables
    private Rigidbody2D rb;
    private Vector3  move;
    // References to other objects (leave public)
    [Header("References")]
    public GameObject topPlatform;
    public GameObject redBackground;
    [Space(10)]
    // Ladder booleans (make private at the end)
    [Header("Ladder booleans")]
    public bool onLadder;
    public bool onGround;
    public bool onNoJumpArea;
    public bool canGoUp;
    public bool canGoDown;
    public bool onTop;
    //
    public SpriteRenderer GateHighlight;
    public Camera Camera;
    public GameObject OptionsRoom;
    public GameObject PlayerCustomizationRoom;
    public GameObject MenuRoom;
    Animator Animator;
    public bool moveCameraLeft;
    public bool moveCameraRight;
    public bool moveCameraToCenter;
    float cameraSpeed = 10;
    public float MenuDownHoldTime;
    bool canMove;
    float prevSpeed;
    void Start () {
        // Get rigidbody of the player at start
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        MenuDownHoldTime = 0;
        prevSpeed = playerSpeed;
    }

	void Update () {
        if (canMove)
        {
            // Create a ray down checking if there is anything underneath the player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.57f);
            // If ray hit something player is on ground
            if (hit.collider != null)
            {
                onGround = true;
            }
            // otherwise the player is in the air
            else
            {
                onGround = false;
            }
            // If player is on a ladder can go down -> go down
            if ((onLadder) && (canGoDown) && ((Input.GetAxis("Vertical") < 0)))
            {
                move = new Vector3(0, Input.GetAxis("Vertical"), 0);
                transform.position += move * playerClimbSpeed * Time.deltaTime;
            }
            // If player is on a ladder and not at the top and can go up -> go up
            if ((onLadder) && (canGoUp) && ((Input.GetAxis("Vertical") > 0)) && (!onTop))
            {
                move = new Vector3(0, Input.GetAxis("Vertical"), 0);
                transform.position += move * playerClimbSpeed * Time.deltaTime;
            }
        
            // Create move vector
            move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            // Move the player
            
                transform.position += move * playerSpeed * Time.deltaTime;
            if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f)
            {
                Animator.SetBool("Running", false);
                Animator.SetBool("Idle", true);
            }

            if (Input.GetAxis("Horizontal") < -0.1f)
            {
                Animator.SetBool("Running", true);
                Animator.SetBool("Idle", false);
                this.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            if (Input.GetAxis("Horizontal") > 0.1f)
            {
                Animator.SetBool("Running", true);
                Animator.SetBool("Idle", false);
                this.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

            //play animations and chaneg sprite rotation
            

            // If player is on ground and space button hit -> jump
            if (Input.GetKeyDown(KeyCode.Space) && (onGround) && (!onNoJumpArea))
            {
                rb.AddForce(Vector2.up * playerJumpForce, ForceMode2D.Impulse);
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),
                    topPlatform.GetComponent<Collider2D>(), false);
            }
            // Special Ability
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (redBackground.activeSelf)
                {
                    redBackground.SetActive(false);
                    return;
                }
                if (!redBackground.activeSelf)
                {
                    redBackground.SetActive(true);
                    return;
                }
            }

            //go to main menu
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene(1);
            }
            if(moveCameraLeft ||moveCameraRight || moveCameraToCenter)
        {
            canMove = false;
        }
            else
        {
            canMove = true;
        }
        //pan camera on main menu screen
        if (moveCameraLeft == true)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, OptionsRoom.transform.position, cameraSpeed * Time.deltaTime);
            canMove = false;
            transform.position += Vector3.left * playerSpeed/4 * Time.deltaTime;
            if (Camera.transform.position==OptionsRoom.transform.position)
            {
                moveCameraLeft = false;
                canMove = true;
            }
        }

        if (moveCameraRight == true)
        {
            Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, PlayerCustomizationRoom.transform.position, cameraSpeed * Time.deltaTime);
            canMove = false;
            transform.position += Vector3.right * playerSpeed / 4 * Time.deltaTime;
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
            transform.position += direction* playerSpeed / 4 * Time.deltaTime;
            if (Camera.transform.position == MenuRoom.transform.position)
            {
                moveCameraToCenter = false;
            }
        }
        //test death animation
        if (Input.GetKey(KeyCode.Q))
        {
            Animator.SetBool("Dead", true);
        }

        if (Input.GetKey(KeyCode.E))
        {
            Animator.SetBool("Dead", false);
        }
        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // While player is at the bottom of a ladder they can't go down and can go up
        if (other.gameObject.tag == "BotLadder")
        {
            canGoDown = false;
            canGoUp = true;
        }
        // Make player's mass 0 if they are on ladder
        if (other.tag == "Ladder")
        {
            rb.gravityScale = 0;
        }
        // At the top of a ladder turn off the collision of a player and the platform above them
        if (other.tag == "TopLadder")
        {
            canGoUp = true;
            onLadder = true;
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), topPlatform.GetComponent<Collider2D>(), true);
        }

        //change highlight on play button and change level on button input
        if (other.gameObject.tag == "Gate")
        {
            GateHighlight.enabled = true;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                SceneManager.LoadScene(2);
            }
            if(Input.GetKey(KeyCode.DownArrow))
            {
                MenuDownHoldTime += Time.deltaTime;          
            }
            if (MenuDownHoldTime > 5)
            {
                SceneManager.LoadScene(3);
            }
            if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                MenuDownHoldTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Top")
        {
            onTop = true;
        }
        // Player cant jump on ladder, can go up and down. Gravity scale = 0 so they can climb up
        if (other.tag == "NoJumpArea")
        {
            onNoJumpArea = true;
            canGoUp = true;
            canGoDown = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.gravityScale = 0;
        }
        // When player enters a ladder make his velocity 0 and turn off gravity scale
        if (other.tag == "Ladder")
        {
            rb.velocity = new Vector3(0, 0, 0);
            onLadder = true;
            rb.gravityScale = 0;
        }
        // At the bottom of a ladder player can go up, but not down
        if (other.gameObject.tag == "BotLadder")
        {
            canGoDown = false;
            canGoUp = true;
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
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Top")
        {
            onTop = false;
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

        if (other.tag == "NoJumpArea")
        {
            onNoJumpArea = false;
            rb.gravityScale = 1;
            canGoUp = false;
            canGoDown = false;
        }
        if (other.tag == "Ladder")
        {
            onLadder = false;
            rb.gravityScale = 1;
        }
        if (other.gameObject.tag == "BotLadder")
        {
            canGoDown = true;
        }
        if (other.gameObject.tag == "TopLadder")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), topPlatform.GetComponent<Collider2D>(), false);
            rb.gravityScale = 1;
        }

        GateHighlight.enabled = false;
    }
}
