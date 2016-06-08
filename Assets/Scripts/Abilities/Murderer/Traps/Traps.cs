using UnityEngine;
using System.Collections;

// Script for murderer allowing him to set traps
public class Traps : MonoBehaviour {

    public int trapsAvailable = 2; // The amount of traps available to murderer to set
    public float trapCooldown;     // Time that must pass before murderer can set next trap
    public float timeSinceSet;     // Time since last trap was set

	void Start ()
    {
        timeSinceSet = 0;
	}
	
	void Update ()
    {
        // If murderer press T and they have a trap available and there's no cooldown -> set a trap
        // on the ground where murderer is
        if ((Input.GetKeyDown(KeyCode.T)) && (trapsAvailable > 0) &&(timeSinceSet<=0)) 
        {
            GameObject trap = Instantiate(Resources.Load("Trap") as GameObject);
            trap.transform.position = new Vector3(
                gameObject.transform.position.x, 
                gameObject.transform.position.y - 1, 
                gameObject.transform.position.z);
            trapsAvailable--;
            timeSinceSet = trapCooldown;
        }
        // Counting time since the trap was set preventing murderer from setting a new trap
        if(timeSinceSet>0)
        {
            timeSinceSet -= Time.deltaTime;
        }
	}
}
