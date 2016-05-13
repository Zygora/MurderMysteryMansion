using UnityEngine;
using System.Collections;

public class ladder : MonoBehaviour {
	public GameObject playerObject;
	public bool canClimb;
	float speed = 1;

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if(coll.gameObject == playerObject)
			canClimb = true;
	}

	void OnCollisionExit2d(Collision2D coll2)
	{
		if (coll2.gameObject == playerObject)
			canClimb = false;
	}
	// Update is called once per frame
	void Update () {
		if (canClimb) {
			if (Input.GetKey (KeyCode.W)) {
				playerObject.transform.Translate (new Vector3 (0, 1, 0) * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.S)) {
				playerObject.transform.Translate (new Vector3 (0, -1, 0) * Time.deltaTime * speed);
			}
		}
	}
}
