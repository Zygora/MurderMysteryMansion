using UnityEngine;
using System.Collections;

// Script for murderer allowing him to set traps
public class Traps : MonoBehaviour {

    public int trapsAvailable = 2; // The amount of traps available to murderer to set
    public float trapCooldown = 10;     // Time that must pass before murderer can set next trap
    public float timeSinceSet;     // Time since last trap was set
    private float refreshTime;
    public float trapChargeTime = 15;
    private string ability;
    private float trapOffset = -8.5f;
    public float trapTime = 2;
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

        gameObject.GetComponent<Controls>().Trapper = true;
        Controls.trappedTime = trapTime;
    }

  

    void Update ()
    {
        if (trapsAvailable < 2)
        {
            refreshTime += Time.deltaTime;
            if (refreshTime >= trapChargeTime)
            {
                trapsAvailable++;
                refreshTime = 0;
            }
        }
        // If murderer press T and they have a trap available and there's no cooldown -> set a trap
        // on the ground where murderer is
        if ((Input.GetButtonDown(ability)) && (trapsAvailable > 0) &&(timeSinceSet<=0)) 
        {
            if (gameObject.tag == "Murderer1" && Controls.player1NoDropOrbZone == false)
            {
                Instantiate(Resources.Load("Trap"),this.transform.position +(transform.up* trapOffset),Quaternion.identity);
                trapsAvailable--;
                timeSinceSet = trapCooldown;
            }

            if (gameObject.tag == "Murderer2" && Controls.player2NoDropOrbZone == false)
            {
                Instantiate(Resources.Load("Trap"), this.transform.position + (transform.up * trapOffset), Quaternion.identity);
                trapsAvailable--;
                timeSinceSet = trapCooldown;
            }

            if (gameObject.tag == "Murderer3" && Controls.player3NoDropOrbZone == false)
            {
                Instantiate(Resources.Load("Trap"), this.transform.position + (transform.up * trapOffset), Quaternion.identity);
                trapsAvailable--;
                timeSinceSet = trapCooldown;
            }

            if (gameObject.tag == "Murderer4" && Controls.player4NoDropOrbZone == false)
            {
                Instantiate(Resources.Load("Trap"), this.transform.position + (transform.up * trapOffset), Quaternion.identity);
                trapsAvailable--;
                timeSinceSet = trapCooldown;
            }
        }
        // Counting time since the trap was set preventing murderer from setting a new trap
        if(timeSinceSet>0)
        {
            timeSinceSet -= Time.deltaTime;
        }
	}
}
