using UnityEngine;
using System.Collections;

// Script for murderer allowing him to set traps
public class Traps : MonoBehaviour {

    public int trapsAvailable = 2; // The amount of traps available to murderer to set
    public float trapCooldown = 10;     // Time that must pass before murderer can set next trap
    public float timeSinceSet;     // Time since last trap was set
    private string ability;
	void Start ()
    {
        timeSinceSet = 0;
        //set input from input manager
        if (gameObject.tag == "Murderer1")
        {
            ability = "Ability_P1";
        }

        if (gameObject.tag == "Murderer2")
        {
            ability = "Ability_P2";
        }

        if (gameObject.tag == "Murderer3")
        {
            ability = "Ability_P3";
        }

        if (gameObject.tag == "Murderer4")
        {
            ability = "Ability_P4";
        }
    }

  

    void Update ()
    {
        // If murderer press T and they have a trap available and there's no cooldown -> set a trap
        // on the ground where murderer is
        if ((Input.GetButtonDown(ability)) && (trapsAvailable > 0) &&(timeSinceSet<=0)) 
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
