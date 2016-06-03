 using UnityEngine;
using System.Collections;

public class FlipSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}
