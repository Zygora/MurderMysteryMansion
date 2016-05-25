using UnityEngine;
using System.Collections;

public class ThirdEye : MonoBehaviour {
    private float time = 0;
    private float cooldown = 0;

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update() {

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > time + cooldown)
            {
                Debug.Log("Third Eye Used");
                time = Time.time;
                cooldown = 5;
            }
	}


}
