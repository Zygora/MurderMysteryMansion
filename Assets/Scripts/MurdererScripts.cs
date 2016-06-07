using UnityEngine;
using System.Collections;

public class MurdererScripts : MonoBehaviour {
    public Animator TorsoAnimator;
    
    public static bool isMurderer;

    // Use this for initialization
    void Start () {
        isMurderer = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f)
        {
            TorsoAnimator.SetBool("MurdererRunning", false);
            TorsoAnimator.SetBool("MurdererIdle", true);
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            TorsoAnimator.SetBool("MurdererRunning", true);
            TorsoAnimator.SetBool("MurdererIdle", false);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            TorsoAnimator.SetBool("MurdererRunning", true);
            TorsoAnimator.SetBool("MurdererIdle", false);
        }
    }
}
