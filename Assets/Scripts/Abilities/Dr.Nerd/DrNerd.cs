using UnityEngine;
using System.Collections;

public class DrNerd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //set bool for affected player
        this.gameObject.GetComponent<Controls>().drNerd = true;
	}
	
	
}
