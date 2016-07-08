using UnityEngine;
using System.Collections;

public class ShittyPossum : MonoBehaviour {
    private string ability;
    void Start()
    {
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
    // Update is called once per frame
    void Update () {

	    if(Input.GetButtonDown(ability))
        {
            // Change murderer's animation state to dead
            // They can't move
            // Controls come back after a second after any button is pressed
        }
	}
}
