using UnityEngine;
using System.Collections;

public class ArrowDestruction : MonoBehaviour {

    float timePassed;               // Time since this arrow was spawned
    float timeBeforeDestroyed = 5;  // Time after which this arrow will be destroyed

	void Update () {
        timePassed += Time.deltaTime;
        if(timePassed>timeBeforeDestroyed)
        {
            //Destroy arrow
            Destroy(gameObject);
        }
	}
}
