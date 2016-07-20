using UnityEngine;
using System.Collections;

public class Masterofdisguise : MonoBehaviour {
    bool nearDownedWimp;
    GameObject downedWimp;
    private string ability;
	// Use this for initialization
	void Start ()
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
	void Update ()
    {
        if (Input.GetButtonDown(ability))
        {
            // If pressed near downed wimp copy their clothes colors
            gameObject.GetComponent<SpriteRenderer>().color = downedWimp.GetComponent<SpriteRenderer>().color;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "DownedWimp")
        {
            nearDownedWimp = true;
            downedWimp = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "DownedWimp")
        {
            nearDownedWimp = false;
            downedWimp = null;
        }
    }
}
