using UnityEngine;
using System.Collections;

public class TurnOnBloodShirt : MonoBehaviour {
    private Animator Aniamtor;
	// Use this for initialization
	void Start () {
        Aniamtor = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (MurdererScripts.isMurderer == true)
        {
            Aniamtor.enabled = true;
        }
    }
}
