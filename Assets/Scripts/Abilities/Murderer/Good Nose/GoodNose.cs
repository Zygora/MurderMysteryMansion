using UnityEngine;
using System.Collections;

public class GoodNose : MonoBehaviour {

    public float cooldown;
    public float timeSinceUsed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Show trails
        }
    }
}
