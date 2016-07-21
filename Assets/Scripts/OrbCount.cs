using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrbCount : MonoBehaviour {
    private int carryingOrbs;
    public Text orbCount;
    public static bool wimpsWin = false;
    public static int wimpsExited = 0;
    public GameObject Orb;
    public static int orbsAtAltar1;
    public static int orbsAtAltar2;
    public static int orbsAtAltar3;
    public string interact;
    public static bool player1CarryOrb = false;
    public static bool player2CarryOrb = false;
    public static bool player3CarryOrb = false;
    public static bool player4CarryOrb = false;
    private bool canDropOrb = false;
    public static bool player1Exited = false;
    public static bool player2Exited = false;
    public static bool player3Exited = false;
    public static bool player4Exited = false;
    
    // Use this for initialization
    void Start () {
        //set amount of orbs carried to 0
        carryingOrbs = 0;
        orbsAtAltar1 = 0;
        orbsAtAltar2 = 0;
        orbsAtAltar3 = 0;

        //set input in input manager
        if (gameObject.tag == "Player1")
        {
            interact = "Interact_P1";
        }

        if (gameObject.tag == "Player2")
        {
            interact = "Interact_P2";
        }

        if (gameObject.tag == "Player3")
        {
            interact = "Interact_P3";
        }

        if (gameObject.tag == "Player4")
        {
            interact = "Interact_P4";
        }
    }
	
	// Update is called once per frame
	void Update () {
        //update orb ui text
        //orbCount.text = "Orb Count: " + carryingOrbs;

        //drop orb if carrying orb
        if (carryingOrbs == 1 && Input.GetButtonDown(interact) && canDropOrb) {
            Instantiate(Resources.Load("Orb"), this.transform.position + transform.up * 28, Quaternion.identity);
            carryingOrbs = 0;
            Debug.Log("orb dropped");
            canDropOrb = false;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //collision with orb only with not currently holding any orbs
        if (gameObject.tag == "Player1" || gameObject.tag == "Player2" || gameObject.tag == "Player3" || gameObject.tag == "Player4")
        {
            if (carryingOrbs == 0 && other.gameObject.tag == "Orb" && Input.GetButtonDown(interact))
            {
                Debug.Log("orb picked up");
                carryingOrbs += 1;
                Destroy(other.gameObject);
                //keeps track of player carrying orb globally
                if (gameObject.tag == "Player1")
                {
                    player1CarryOrb = true;
                }

                if (gameObject.tag == "Player2")
                {
                    player2CarryOrb = true;
                }

                if (gameObject.tag == "Player3")
                {
                    player3CarryOrb = true;
                }

                if (gameObject.tag == "Player4")
                {
                    player4CarryOrb = true;
                }
                //allowing dropping of orb after delay
                Invoke("CanDropOrb", 2f);
            }
        }

        //place orb at altar 1
        if (other.gameObject.tag == "Altar1")
        {
            if (carryingOrbs == 1 && orbsAtAltar1 == 0 && Input.GetButtonDown(interact))
            {
                Instantiate(Orb, other.gameObject.transform.position + transform.up * 20f, Quaternion.identity);
                carryingOrbs -= 1;
                //keeps track of player carrying orb globally
                if (gameObject.tag == "Player1")
                {
                    player1CarryOrb = false;
                }

                if (gameObject.tag == "Player2")
                {
                    player2CarryOrb = false;
                }

                if (gameObject.tag == "Player3")
                {
                    player3CarryOrb = false;
                }

                if (gameObject.tag == "Player4")
                {
                    player4CarryOrb = false;
                }
                orbsAtAltar1 += 1;
            }
        }

        //place orb at altar 2
        if (other.gameObject.tag == "Altar2")
        {
            if (carryingOrbs == 1 && orbsAtAltar2 == 0 && Input.GetButtonDown(interact))
            {
                Instantiate(Orb, other.gameObject.transform.position + transform.up * 20f, Quaternion.identity);
                carryingOrbs -= 1;
                //keeps track of player carrying orb globally
                if (gameObject.tag == "Player1")
                {
                    player1CarryOrb = false;
                }

                if (gameObject.tag == "Player2")
                {
                    player2CarryOrb = false;
                }

                if (gameObject.tag == "Player3")
                {
                    player3CarryOrb = false;
                }

                if (gameObject.tag == "Player4")
                {
                    player4CarryOrb = false;
                }
                orbsAtAltar2 += 1;
            }
        }

        //place orb at altar 3
        if (other.gameObject.tag == "Altar3")
        {
            if (carryingOrbs == 1 && orbsAtAltar3 == 0 && Input.GetButtonDown(interact))
            {
                Instantiate(Orb, other.gameObject.transform.position + transform.up * 20f, Quaternion.identity);
                carryingOrbs -= 1;
                //keeps track of player carrying orb globally
                if (gameObject.tag == "Player1")
                {
                    player1CarryOrb = false;
                }

                if (gameObject.tag == "Player2")
                {
                    player2CarryOrb = false;
                }

                if (gameObject.tag == "Player3")
                {
                    player3CarryOrb = false;
                }

                if (gameObject.tag == "Player4")
                {
                    player4CarryOrb = false;
                }
                orbsAtAltar3 += 1;
            }
        }

        //interaction with exit
        if (other.gameObject.tag == "WimpExit") {
            if(orbsAtAltar1 == 1 && orbsAtAltar2 == 1 && orbsAtAltar3 == 1 && Input.GetButtonDown(interact))
            {
                //increment wimps exited needed for wimp win condition
                wimpsExited += 1;

                if (gameObject.tag == "Player1" && player1Exited == false)
                {
                    player1Exited = true;
                }

                if (gameObject.tag == "Player2" && player2Exited == false)
                {
                    player2Exited = true;
                }

                if (gameObject.tag == "Player3" && player3Exited == false)
                {
                    player3Exited = true;
                }

                if (gameObject.tag == "Player4" && player4Exited == false)
                {
                    player4Exited = true;
                }
            }
        }
    }

    void CanDropOrb() {
        canDropOrb = true;
    }
}
