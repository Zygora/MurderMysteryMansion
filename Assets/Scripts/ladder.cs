using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {
	public GameObject upPlatform;
	private Movement movement;
	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			other.gameObject.GetComponent<Movement> ().topPlatform = upPlatform;
		}
	}
}
