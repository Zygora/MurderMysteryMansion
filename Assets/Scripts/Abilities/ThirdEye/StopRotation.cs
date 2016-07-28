using UnityEngine;
using System.Collections;

public class StopRotation : MonoBehaviour {
    private Vector3 rotation;
	// Use this for initialization
	void Awake () {
        rotation = new Vector3(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.eulerAngles = rotation;
	}
}
