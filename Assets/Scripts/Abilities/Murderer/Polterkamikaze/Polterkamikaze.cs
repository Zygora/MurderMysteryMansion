using UnityEngine;
using System.Collections;

public class Polterkamikaze : MonoBehaviour {
    public GameObject animateObject;
    public bool canControl;
    private string ability;
    void Start() {
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
	    if(Input.GetButtonDown(ability)&&(canControl))
        {
            // Control an object and kill a wimp and the murderer
            canControl = false;
        }
	}
}
