using UnityEngine;
using System.Collections;

public class GoodNose : MonoBehaviour {

    public float cooldown;
    public float timeSinceUsed;
    private string ability;
	// Use this for initialization
	void Start () {
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
        if (Input.GetButtonDown(ability))
        {
            // Show trails
        }
    }
}
