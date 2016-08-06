using UnityEngine;
using System.Collections;
//  Script for a trap
public class Trap : MonoBehaviour {

    public float trapsLifeSeconds = 30.0f;
    float timeSinceSet;

	void Start ()
    {
        timeSinceSet = 0;
	}
	
	void Update ()
    {
        // Count time since this trap was set
        timeSinceSet += Time.deltaTime;
        // If time since it was set is more than trap life time -> destroy this trap
        if(timeSinceSet > trapsLifeSeconds)
        {
            Destroy(gameObject);
        }
	}

  
}
