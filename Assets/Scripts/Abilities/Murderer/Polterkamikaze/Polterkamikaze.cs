using UnityEngine;
using System.Collections;

public class Polterkamikaze : MonoBehaviour {
    public GameObject animateObject;
    public bool canControl;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.E)&&(canControl))
        {
            // Control an object and kill a wimp and the murderer
            canControl = false;
        }
	}
}
