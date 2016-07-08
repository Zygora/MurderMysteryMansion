using UnityEngine;
using System.Collections;

public class TurnOnBloodShirt : MonoBehaviour {
    private Animator Aniamtor;
    public GameObject player;
	// Use this for initialization
	void Start () {
        Aniamtor = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        //change tag of game object this script is atatched to so it can be accessed in other script
        if (player.tag == "Murderer1")
        {
            gameObject.tag = "MurdererShirt1";
        }

        if (player.tag == "Murderer2")
        {
            gameObject.tag = "MurdererShirt2";
        }

        if (player.tag == "Murderer3")
        {
            gameObject.tag = "MurdererShirt3";
        }

        if (player.tag == "Murderer4")
        {
            gameObject.tag = "MurdererShirt4";
        }
       
    }
}
