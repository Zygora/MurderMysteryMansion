﻿using UnityEngine;
using System.Collections;

public class TurnOnBloodShirt : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        //change tag of game object this script is atatched to so it can be accessed in other script
        if (player.tag == "Murderer1")
        {
            gameObject.tag = "MurdererShirt1";
            gameObject.GetComponent<Animator>().enabled = true;
        }

        if (player.tag == "Murderer2")
        {
            gameObject.tag = "MurdererShirt2";
            gameObject.GetComponent<Animator>().enabled = true;
        }

        if (player.tag == "Murderer3")
        {
            gameObject.tag = "MurdererShirt3";
            gameObject.GetComponent<Animator>().enabled = true;
        }

        if (player.tag == "Murderer4")
        {
            gameObject.tag = "MurdererShirt4";
            gameObject.GetComponent<Animator>().enabled = true;
        }
       
    }
}
