using UnityEngine;
using System.Collections;

// Script for murderer allowing him to set traps
public class Traps : MonoBehaviour {
    
    public float trapCooldown = 10;     // Time that must pass before murderer can set next trap
    public float timeSinceSet;     // Time since last trap was set
    private float trapOffset = -8.5f;
    public float trapTime = 2;

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

        gameObject.GetComponent<Controls>().Trapper = true;
        Controls.trappedTime = trapTime;
    }

  

    void Update ()
    {
        // If murderer press T and they have a trap available and there's no cooldown -> set a trap
        // on the ground where murderer is
        if ((Input.GetButtonDown(ability)) &&(timeSinceSet<=0)) 
        {
            if (gameObject.tag == "Murderer1" && Controls.player1NoDropOrbZone == false)
            {
                CreateTrap();
            }

            if (gameObject.tag == "Murderer2" && Controls.player2NoDropOrbZone == false)
            {
                CreateTrap();
            }

            if (gameObject.tag == "Murderer3" && Controls.player3NoDropOrbZone == false)
            {
                CreateTrap();
            }

            if (gameObject.tag == "Murderer4" && Controls.player4NoDropOrbZone == false)
            {
                CreateTrap();
            }
        }
        // Counting time since the trap was set preventing murderer from setting a new trap
        if(timeSinceSet>0)
        {
            timeSinceSet -= Time.deltaTime;
        }
	}

    //FUNCTIONS
    void CreateTrap()
    {
        Instantiate(Resources.Load("Trap"), this.transform.position + (transform.up * trapOffset), Quaternion.identity);
        timeSinceSet = trapCooldown;
    }
}
