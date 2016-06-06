using UnityEngine;
using System.Collections;

// Move to Controls script and disable player's movement while he's reviving
public class Revive : MonoBehaviour
{
    bool nearDownedWimp;
    public float reviveTime = 2.0f;
    float reviveTimePassed;
    GameObject downedWimp;

    void Update()
    {
        if ((nearDownedWimp) && (Input.GetKey(KeyCode.R)))
        {
            reviveTimePassed += Time.deltaTime;
            if (reviveTimePassed > reviveTime)
            {
                downedWimp.tag = "Wimp";
                downedWimp.GetComponent<Animator>().enabled = true;
                reviveTimePassed = 0;
                nearDownedWimp = false;
                print("revived");
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "DownedWimp")
        {
            nearDownedWimp = true;
            downedWimp = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "DownedWimp")
        {
            nearDownedWimp = false;
        }
    }
}
