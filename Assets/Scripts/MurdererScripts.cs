﻿using UnityEngine;
using System.Collections;

public class MurdererScripts : MonoBehaviour {
    public Animator ShirtAnimator;
    public Animator TorsoAnimator;
    public Animator LegsAnimator;
   
    public static bool isMurderer;

    // Use this for initialization
    void Start () {
        isMurderer = true;
        gameObject.tag = "Murderer";
        gameObject.layer = 9;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f)
        {
            ShirtAnimator.SetBool("MurdererRunning", false);
            ShirtAnimator.SetBool("MurdererIdle", true);
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            ShirtAnimator.SetBool("MurdererRunning", true);
            ShirtAnimator.SetBool("MurdererIdle", false);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            ShirtAnimator.SetBool("MurdererRunning", true);
            ShirtAnimator.SetBool("MurdererIdle", false);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            TorsoAnimator.Play("TorsoMurdererAttack");
            LegsAnimator.Play("LegsMurdererAttack");
            ShirtAnimator.Play("MurdererAttack");
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        // Debug.DrawRay(transform.position, Vector2.down, Color.red);
        // If ray hit something player is on ground
        if (hit.collider != null) ;
        {
            if (hit.collider.tag == "Ground" && hit.distance < 1.3f)
            {
                // Debug.Log(hit.distance);
                ShirtAnimator.SetBool("MurdererJumping", false);
            }
            // otherwise the player is in the air
            else if (hit.distance >= 1.3f)
            {
                // Debug.Log(hit.distance);
                ShirtAnimator.SetBool("MurdererJumping", true);
            }
        }
    }
}
