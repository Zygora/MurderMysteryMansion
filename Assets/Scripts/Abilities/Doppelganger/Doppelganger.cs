using UnityEngine;
using System.Collections;

public class Doppelganger : MonoBehaviour
{
    public int clonesAvailable = 2;      // The amount of clones available to player
    public float cooldown = 10;           // Cooldown time player has to wait before he is able to summon another clone
    public float cloneSpeed = 20;
    Vector2 direction;               // Direction in which clone and arrow will be moving
    float timeSinceUsed;             // Time passed since the ability was last used
    public bool cloneUsed = false;                  // Flag that shows that the ability was used
    private string ability;
    public static bool cloneMoving = false;
    public static bool cloneAlive = false;
    private float cloneRefreshStart;
    public float cloneRefreshTime = 15;

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
    void Update()
    {
        if (Input.GetButtonDown(ability) && cloneMoving == true)
        {
            cloneMoving = false;
        }

        if (clonesAvailable < 2) {
            cloneRefreshStart += Time.deltaTime;
            if (cloneRefreshStart > cloneRefreshTime)
            {
                clonesAvailable++;
                cloneRefreshStart = 0;
            }
        }

        if (cloneUsed == true) {
            timeSinceUsed += Time.deltaTime;
            if(timeSinceUsed> cooldown)
            {
                cloneUsed = false;
                timeSinceUsed = 0;
            }
        }

        // If ability can be used
        if (cloneAlive == false && cloneUsed == false)
        {
            // If there's another clone charge available
            if (clonesAvailable > 0)
            {
                // Player faces right
                if (Input.GetButtonDown(ability) && (gameObject.GetComponent<Controls>().direction > 0))
                {
                    SetBoolsAndDirection();
                    GameObject clone = Instantiate(Resources.Load("Clone"), this.transform.position, Quaternion.identity) as GameObject;
                    clone.gameObject.GetComponent<CloneScript>().startPosition = this.transform.position;
                    clone.gameObject.GetComponent<CloneScript>().direction = direction;
                    clone.gameObject.GetComponent<CloneScript>().cloneSpeed = cloneSpeed;
                    clone.transform.eulerAngles = new Vector3(0, 0, 0);
                }

                // Player faces left
                else if (Input.GetButtonDown(ability) && (gameObject.GetComponent<Controls>().direction < 0))
                {
                    SetBoolsAndDirection();
                    GameObject clone = Instantiate(Resources.Load("Clone"), this.transform.position, Quaternion.identity) as GameObject;
                    clone.gameObject.GetComponent<CloneScript>().startPosition = this.transform.position;
                    clone.gameObject.GetComponent<CloneScript>().direction = direction;
                    clone.gameObject.GetComponent<CloneScript>().cloneSpeed = cloneSpeed;
                    clone.transform.eulerAngles = new Vector3(0, 180, 0);
                }
            }
        }
    }

    void SetBoolsAndDirection() {
        direction = new Vector2(1, 0);
        cloneMoving = true;
        cloneAlive = true;
        cloneUsed = true;
        clonesAvailable--;
    }
}
