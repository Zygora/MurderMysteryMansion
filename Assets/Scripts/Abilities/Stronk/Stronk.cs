using UnityEngine;

public class Stronk : MonoBehaviour
{

    public GameObject player;               // Reference to the player that can be trown
    public Vector2 throwDirectionRight;     // Direction to throw a wimp to the right
    public Vector2 throwDirectionLeft;      // Direction to throw a wimp to the left
    public float throwForce;                // Force of the throw

    bool nearWimp;                          // Flag showing that this wimp is near another one
    bool grabbed;                           // Flag showing if this wimp is holding to another one
    private string ability;
    void Start() {
        //set input from input manager
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
    }
    // Update is called once per frame
    void Update()
    {
        // If player is near a wimp and presses E
        if ((Input.GetButtonDown(ability)) && (nearWimp))
        {
            // Grab a wimp
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            grabbed = true;
            player.transform.position = 
                new Vector3(
                gameObject.transform.position.x - 0.3f, 
                gameObject.transform.position.y + 1.9f, 
                gameObject.transform.position.z
                );
            // Rotate the wimp that this wimp holds on to to horizontal position
            player.transform.rotation = Quaternion.Euler(0, 0, 90);
            // Make the wimp that is in the air a child to this wimp so they will move together
            player.transform.parent = gameObject.transform;
        }
        // If player holds a wimp and presses R
        if ((Input.GetButtonDown(ability)) && (grabbed))
        {
            // Throw the wimp
            player.transform.parent = null;
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            // If player is facing left trow to the left
            if (gameObject.GetComponent<Controls>().direction < 0)
            {
                player.GetComponent<Rigidbody2D>().AddForce(throwDirectionLeft * throwForce, ForceMode2D.Impulse);
            }
            // If player is facing right trow to the right
            else
            {
                player.GetComponent<Rigidbody2D>().AddForce(throwDirectionRight * throwForce, ForceMode2D.Impulse);
            }
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            grabbed = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wimp")
        {
            nearWimp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wimp")
        {
            nearWimp = false;
        }
    }
}
