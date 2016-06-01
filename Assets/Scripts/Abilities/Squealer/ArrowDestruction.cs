using UnityEngine;
using System.Collections;

public class ArrowDestruction : MonoBehaviour {
    float timePassed;
    float timeBeforeDestroyed = 5;

	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        if(timePassed>timeBeforeDestroyed)
        {
            Destroy(gameObject);
        }
	}
}
