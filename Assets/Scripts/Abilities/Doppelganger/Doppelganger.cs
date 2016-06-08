using UnityEngine;
using System.Collections;

public class Doppelganger : MonoBehaviour
{
    public int clonesAvailable = 2; // The amount of clones that can be used
    public float cooldown;          // Time after which another clon can be summoned
    public float timeSinceUsed;     // Time since last clon was summoned

    // Update is called once per frame
    void Update()
    {
        if(timeSinceUsed>0)
        {
            timeSinceUsed -= Time.deltaTime;
        }
        // If player presses E and they have clones available and the ability is not on cooldown -> summon clone
        if ((Input.GetKeyDown(KeyCode.E)) && (clonesAvailable > 0)&&(timeSinceUsed<=0)) 
        {
            GameObject clone = Instantiate(Resources.Load("Clone") as GameObject);
            clone.transform.position = gameObject.transform.position;
            clonesAvailable--;
            timeSinceUsed = cooldown;
        }
    }
}
