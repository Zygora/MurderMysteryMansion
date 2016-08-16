using UnityEngine;
using System.Collections;

public class ShittyPossum : MonoBehaviour {
    private string ability;

    public static bool possumed = false;
    public bool unPossum = false;
    public bool canPossum = true; 

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
        if (Input.GetButtonDown(ability) && unPossum == true)
        {
            //turn on colliders
            gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            gameObject.GetComponent<Collider2D>().isTrigger = false;
            //turn on bloody shirt animtors
            BloodShirtToggle(true);
            possumed = false;
            unPossum = false;
            Invoke("CanPossum", 1f);
        }

        if (Input.GetButtonDown(ability) && canPossum == true)
        {
            // turn off colliders
            gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            //turn off bloody shirt animators
            BloodShirtToggle(false);
            possumed = true;
            canPossum = false;
            // Controls come back after a second after any button is pressed
            Invoke("CanUnPossum", 1f);
        }
	}

    //FUCNTIONS
    void CanUnPossum() {
        unPossum = true;
    }

    void CanPossum()
    {
        canPossum = true;
    }

    void BloodShirtToggle(bool bloody)
    {
        //turn off bloody shirt animators
        if (gameObject.tag == "Murderer1")
        {
            GameObject.FindGameObjectWithTag("MurdererShirt1").GetComponent<Animator>().enabled = bloody;
        }
        if (gameObject.tag == "Murderer2")
        {
            GameObject.FindGameObjectWithTag("MurdererShirt2").GetComponent<Animator>().enabled = bloody;
        }
        if (gameObject.tag == "Murderer3")
        {
            GameObject.FindGameObjectWithTag("MurdererShirt3").GetComponent<Animator>().enabled = bloody;
        }
        if (gameObject.tag == "Murderer4")
        {
            GameObject.FindGameObjectWithTag("MurdererShirt4").GetComponent<Animator>().enabled = bloody;
        }
    }
}
