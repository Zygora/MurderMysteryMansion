using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OrbCount : MonoBehaviour {
    private int carryingOrbs;
    public Text orbCount;
    public Text wimpWinText;
    public GameObject Orb;
    public static int orbsAtAltar1;
    public static int orbsAtAltar2;
    public static int orbsAtAltar3;
    // Use this for initialization
    void Start () {
        //set amount of orbs carried to 0
        carryingOrbs = 0;
        orbsAtAltar1 = 0;
        orbsAtAltar2 = 0;
        orbsAtAltar3 = 0;
    }
	
	// Update is called once per frame
	void Update () {
        //update orb ui text
        orbCount.text = "Orb Count: " + carryingOrbs;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //collision with orb only with not currently holding any orbs
        if (MurdererScripts.isMurderer == true && other.gameObject.tag == "Orb")
        {
            if (carryingOrbs == 0)
            {
                carryingOrbs += 1;
                Destroy(other.gameObject);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //place orb at altar 1
        if (other.gameObject.tag == "Altar1")
        {
            if (carryingOrbs == 1 && orbsAtAltar1 == 0)
            {
                Instantiate(Orb, other.gameObject.transform.position + transform.up * .95f, Quaternion.identity);
                carryingOrbs -= 1;
                orbsAtAltar1 += 1;
            }
        }

        //place orb at altar 2
        if (other.gameObject.tag == "Altar2")
        {
            if (carryingOrbs == 1 && orbsAtAltar2 == 0)
            {
                Instantiate(Orb, other.gameObject.transform.position + transform.up * .95f, Quaternion.identity);
                carryingOrbs -= 1;
                orbsAtAltar2 += 1;
            }
        }

        //place orb at altar 3
        if (other.gameObject.tag == "Altar3")
        {
            if (carryingOrbs == 1 && orbsAtAltar3 == 0)
            {
                Instantiate(Orb, other.gameObject.transform.position + transform.up * .95f, Quaternion.identity);
                carryingOrbs -= 1;
                orbsAtAltar3 += 1;
            }
        }

        if (other.gameObject.tag == "WimpExit") {
            if(orbsAtAltar1 == 1 && orbsAtAltar2 == 1 && orbsAtAltar3 == 1 && Input.GetKey(KeyCode.C))
            {
                Debug.Log("Exit");
                wimpWinText.text = "Wimps Win";
            }
        }
    }
}
