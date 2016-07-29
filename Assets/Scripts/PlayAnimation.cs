using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {
   public Animator ShirtAnimator;
    //Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void PlayAnimator() {
       ShirtAnimator.Play("MurdererRunning");
        Debug.Log("Running");
    }
}
