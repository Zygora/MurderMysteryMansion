using UnityEngine;
using System.Collections;

public class DrNerd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Controls>().drNerd = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
