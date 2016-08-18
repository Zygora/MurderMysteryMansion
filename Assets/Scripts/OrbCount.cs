using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrbCount : MonoBehaviour {
    public Text orbCount;

    public static int orbsAtAltar1;
    public static int orbsAtAltar2;
    public static int orbsAtAltar3;
    public static int wimpsExited = 0;
    private int carryingOrbs;

    public string interact;

    public static bool player1CarryOrb = false;
    public static bool player2CarryOrb = false;
    public static bool player3CarryOrb = false;
    public static bool player4CarryOrb = false;
    public static bool player1Exited = false;
    public static bool player2Exited = false;
    public static bool player3Exited = false;
    public static bool player4Exited = false;
    public static bool player1CanEnter = false;
    public static bool player2CanEnter = false;
    public static bool player3CanEnter = false;
    public static bool player4CanEnter = false;
    public static bool doorOpen;
    public static bool wimpsWin = false;
    private bool canDropOrb = false;

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

        //reset values
        player1CarryOrb = false;
        player2CarryOrb = false;
        player3CarryOrb = false;
        player4CarryOrb = false;
        player1Exited = false;
        player2Exited = false;
        player3Exited = false;
        player4Exited = false;
        player1CanEnter = false;
        player2CanEnter = false;
        player3CanEnter = false;
        player4CanEnter = false;
        doorOpen = false;
        wimpsWin = false;
    }
	
	// Update is called once per frame
	void Update () {
        //drop orb if carrying orb
        if (carryingOrbs == 1 && Input.GetButtonDown(interact) && canDropOrb) {
            if (gameObject.tag == "Player1" && Controls.player1NoDropOrbZone == false)
            {
                DropOrb();
                player1CarryOrb = false;
            }

            if (gameObject.tag == "Player2" && Controls.player2NoDropOrbZone == false)
            {
                DropOrb();
                player2CarryOrb = false;
            }

            if (gameObject.tag == "Player3" && Controls.player3NoDropOrbZone == false)
            {
                DropOrb();
                player3CarryOrb = false;
            }

            if (gameObject.tag == "Player4" && Controls.player4NoDropOrbZone == false)
            {
                DropOrb();
                player4CarryOrb = false;
            }
        }

        //open door if orbs placed at altar
        if (orbsAtAltar1 == 1 && orbsAtAltar2 == 1 && orbsAtAltar3 == 1)
        {
            doorOpen = true;
        }

        else {
            doorOpen = false;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //collision with orb only with not currently holding any orbs
        if (gameObject.tag == "Player1" || gameObject.tag == "Player2" || gameObject.tag == "Player3" || gameObject.tag == "Player4")
        {
            if (carryingOrbs == 0 && other.gameObject.tag == "Orb" && Input.GetButtonDown(interact))
            {
                carryingOrbs += 1;
                Destroy(other.transform.parent.gameObject);
                //keeps track of player carrying orb globally
                CarryingOrb(true);
                //allowing dropping of orb after delay
                Invoke("CanDropOrb", 2f);
            }
        }

        //place orb at altar 1
        if (other.gameObject.tag == "Altar1")
        {
            if (carryingOrbs == 1 && orbsAtAltar1 == 0 && Input.GetButtonDown(interact))
            {
                Instantiate(Resources.Load("Orb Placed Left"), other.gameObject.transform.position + transform.up * 45f
                          , Quaternion.identity);
                carryingOrbs -= 1;
                //keeps track of player carrying orb globally
                CarryingOrb(false);
                orbsAtAltar1 += 1;
                canDropOrb = false;
            }
        }

        //place orb at altar 2
        if (other.gameObject.tag == "Altar2")
        {
            if (carryingOrbs == 1 && orbsAtAltar2 == 0 && Input.GetButtonDown(interact))
            {
                Instantiate(Resources.Load("Orb Placed Middle"), other.gameObject.transform.position + transform.up * 45f
                          , Quaternion.identity);
                carryingOrbs -= 1;
                //keeps track of player carrying orb globally
                CarryingOrb(false);
                orbsAtAltar2 += 1;
                canDropOrb = false;
            }
        }

        //place orb at altar 3
        if (other.gameObject.tag == "Altar3")
        {
            if (carryingOrbs == 1 && orbsAtAltar3 == 0 && Input.GetButtonDown(interact))
            {
                Instantiate(Resources.Load("Orb Placed Right"), other.gameObject.transform.position + transform.up * 45f
                          , Quaternion.identity);
                carryingOrbs -= 1;
                //keeps track of player carrying orb globally
                CarryingOrb(false);
                orbsAtAltar3 += 1;
                canDropOrb = false;
            }
        }

        //interaction with exit
        if (other.gameObject.tag == "WimpExit") {
            if(orbsAtAltar1 == 1 && orbsAtAltar2 == 1 && orbsAtAltar3 == 1 && Input.GetButtonDown(interact))
            {
                
                if (gameObject.tag == "Player1" && player1Exited == false)
                {
                    player1Exited = true;
					Invoke ("P1CanReenter", 1);
					//increment wimps exited needed for wimp win condition
					wimpsExited += 1;
                }

                if (gameObject.tag == "Player2" && player2Exited == false)
                {
                    player2Exited = true;
					Invoke ("P2CanReenter", 1);
					//increment wimps exited needed for wimp win condition
					wimpsExited += 1;
                }

                if (gameObject.tag == "Player3" && player3Exited == false)
                {
                    player3Exited = true;
					Invoke ("P3CanReenter", 1);
					//increment wimps exited needed for wimp win condition
					wimpsExited += 1;
                }

                if (gameObject.tag == "Player4" && player4Exited == false)
                {
                    player4Exited = true;
					Invoke ("P4CanReenter", 1);
					//increment wimps exited needed for wimp win condition
					wimpsExited += 1;
                }
            }
        }
    }

    //FUNCTIONS

    void CanDropOrb() {
        canDropOrb = true;
    }

    void P1CanReenter(){
	player1CanEnter = true;
    }

    void P2CanReenter(){
	player2CanEnter = true;
    }

	void P3CanReenter(){
		player3CanEnter = true;
	}

	void P4CanReenter(){
		player4CanEnter = true;
	}

    void DropOrb() {
        Instantiate(Resources.Load("Orb"), this.transform.position + transform.up * 20, Quaternion.identity);
        carryingOrbs = 0;
        canDropOrb = false;
    }

    void CarryingOrb(bool carryingOrb) {

        if (gameObject.tag == "Player1")
        {
            player1CarryOrb = carryingOrb;
        }

        if (gameObject.tag == "Player2")
        {
            player2CarryOrb = carryingOrb;
        }

        if (gameObject.tag == "Player3")
        {
            player3CarryOrb = carryingOrb;
        }

        if (gameObject.tag == "Player4")
        {
            player4CarryOrb = carryingOrb;
        }
    }
}
