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
        if (player.tag == "Murderer1" || player.tag == "Murderer2" || player.tag == "Murderer3" || player.tag == "Murderer4" )
        {
            Aniamtor.enabled = true;
        }
    }
}
