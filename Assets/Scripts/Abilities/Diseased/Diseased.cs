﻿using UnityEngine;
using System.Collections;

public class Diseased : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Controls>().diseased = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
