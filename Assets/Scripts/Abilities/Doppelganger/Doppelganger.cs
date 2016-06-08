using UnityEngine;
using System.Collections;

public class Doppelganger : MonoBehaviour
{
    public int clonesAvailable = 2; // The amount of clones that can be used
    public float cooldown;          // Time after which another clon can be summoned
    public float timeSinceUsed;     // Time since last clon was summoned
    float holdTime;                 // Time for which player is holding E button

    void Update()
    {
        if (timeSinceUsed > 0)
        {
            timeSinceUsed -= Time.deltaTime;
        }
        // Player sharges clone by holding E button
        if ((Input.GetKey(KeyCode.E)) && (clonesAvailable > 0) && (timeSinceUsed <= 0))
        {
            holdTime += Time.deltaTime;
        }
        // If player releases E button and they have clones available and the ability is not on
        // cooldown -> summon clone for duble the time player was holding the button
        if ((Input.GetKeyUp(KeyCode.E)) && (clonesAvailable > 0) && (timeSinceUsed <= 0))
        {
            GameObject clone = Instantiate(Resources.Load("Clone") as GameObject);
            clone.GetComponent<CloneScript>().timeBeforeDestroyed = holdTime * 2;
            clone.GetComponent<Controls>().TorsoAnimator.SetBool("Running", true);
            clone.GetComponent<Controls>().LegsAnimator.SetBool("Running", true);
            clone.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", false);
            clone.GetComponent<Controls>().LegsAnimator.SetBool("Idle", false);
            clone.transform.position = gameObject.transform.position;
            clonesAvailable--;
            timeSinceUsed = cooldown;
            holdTime = 0;
        }
    }
}
