using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {
    public float speed;
    public Rigidbody2D  rb;
    public GameObject red;
    public float jumpForce;
    public GameObject topPlatform;

    Vector3  move;
    public bool onLadder;
    public bool onGround;
    public bool onNoJumpArea;
    public bool canGoUp;
    public bool canGoDown;
    public bool onTop;

    // Use this for initialization
    void Start () {
        // Get rigidbody of the player at start
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
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
        if ((onLadder) && (canGoDown) && ((Input.GetAxis("Vertical") < 0)))
        {
            move = new Vector3(0, Input.GetAxis("Vertical"), 0);
            transform.position += move * speed * Time.deltaTime;
        }

        if ((onLadder) && (canGoUp) && ((Input.GetAxis("Vertical") > 0))&&(!onTop))
        {
            move = new Vector3(0, Input.GetAxis("Vertical"), 0);
            transform.position += move * speed * Time.deltaTime;
        }

        // Create move vector
        move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        // Move the player
        transform.position += move * speed * Time.deltaTime;


        // If player is on ground and space button hit -> jump
        if (Input.GetKeyDown(KeyCode.Space) && (onGround)&&(!onNoJumpArea))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),
                topPlatform.GetComponent<Collider2D>(), false);
        }
        // Special Ability
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (red.activeSelf)
            {
                red.SetActive(false);
                return;
            }
            if (!red.activeSelf)
            {
                red.SetActive(true);
                return;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "BotLadder")
        {
            canGoDown = false;
            canGoUp = true;
        }
        if (other.tag == "Ladder")
        {
            rb.gravityScale = 0;
        }
        if (other.tag == "TopLadder")
        {
            onLadder = true;
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),
               topPlatform.GetComponent<Collider2D>(), true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Top")
        {
            onTop = true;
        }
            if (other.tag == "NoJumpArea")
        {
            onNoJumpArea = true;
            canGoUp = true;
            canGoDown = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.gravityScale = 0;
        }
        if (other.tag=="Ladder")
        {
            onLadder = true;
            rb.gravityScale = 0;
            if (other.gameObject.transform.position.y < gameObject.transform.position.y)
            {
                canGoUp = false;
            }

    }
        if (other.gameObject.tag == "TopLadder")
        {
            canGoDown = true;
        }
        if (other.gameObject.tag == "BotLadder")
        {
            canGoDown = false;
            canGoUp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Top")
        {
            onTop = false;
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
                Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(),
                   topPlatform.GetComponent<Collider2D>(), false);
            rb.gravityScale = 1;
        }
    }

}
