using UnityEngine;
using System.Collections;

public class Diseased : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //diseased bool for affected player
        this.gameObject.GetComponent<Controls>().diseased = true;
	}
}
