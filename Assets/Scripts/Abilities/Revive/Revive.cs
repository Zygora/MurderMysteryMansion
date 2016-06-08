using UnityEngine;
using System.Collections;

// Move to Controls script and disable player's movement while he's reviving
public class Revive : MonoBehaviour
{
    public float reviveTime = 2.0f; // Time which it takes to revive a wimp
    bool nearDownedWimp;            // Flag that shows if this wimp is near another wimp that needs to be revived
    float reviveTimePassed;         // Time since start of the reviving process
    GameObject downedWimp;          // Reference to the wimp that needs to be revived

    void Update()
    {
        // If this wimp is near a wimp that needs to be revived and player presses R key
        if ((nearDownedWimp) && (Input.GetKey(KeyCode.R)))
        {
            // Start counting time since the start of the revivng process
            reviveTimePassed += Time.deltaTime;
            // If player is revivng for enough time -> revive downed wimp
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
        // If this wimp is standing near a downed wimp make the flag true and keep the reference to that wimp
        if (other.gameObject.tag == "DownedWimp")
        {
            nearDownedWimp = true;
            downedWimp = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // When this wimp leaves a downed wimp make the flag false
        if (other.gameObject.tag == "DownedWimp")
        {
            nearDownedWimp = false;
        }
    }
}
