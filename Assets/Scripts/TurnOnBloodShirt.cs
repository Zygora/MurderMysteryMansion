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
        if (player.tag == "Murderer")
        {
            Aniamtor.enabled = true;
        }
    }
}
