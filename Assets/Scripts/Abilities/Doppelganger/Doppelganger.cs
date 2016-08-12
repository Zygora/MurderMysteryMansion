using UnityEngine;
using System.Collections;

public class Doppelganger : MonoBehaviour
{
    public float cooldown = 10;           // Cooldown time player has to wait before he is able to summon another clone
    public float cloneSpeed = 55;
   
    float timeSinceUsed;             // Time passed since the ability was last used

    Vector2 direction;               // Direction in which clone and arrow will be moving

    public static bool cloneMoving = false;
    public static bool cloneAlive = false;
    public bool cloneUsed = false;                  // Flag that shows that the ability was used

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
    void Update()
    {
        //stop clone from moving on button press
        if (Input.GetButtonDown(ability) && cloneMoving == true)
        {
            cloneMoving = false;
        }

        //track doppelganger cooldown
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
                if(gameObject.tag == "Player1" && Controls.player1NoDropOrbZone == false)
                {
                    MakeClone();
                }

                if (gameObject.tag == "Player2" && Controls.player2NoDropOrbZone == false)
                {
                    MakeClone();
                }

                if (gameObject.tag == "Player3" && Controls.player3NoDropOrbZone == false)
                {
                    MakeClone();
                }

                if (gameObject.tag == "Player4" && Controls.player4NoDropOrbZone == false)
                {
                    MakeClone();
                }
        }
    }

    //FUCNTIONS

    void SetBoolsAndDirection() {
        direction = new Vector2(1, 0);
        cloneMoving = true;
        cloneAlive = true;
        cloneUsed = true;
    }

    void MakeClone()
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
