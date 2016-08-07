using UnityEngine;
using System.Collections;

public class ArrowDestruction : MonoBehaviour {

    float timePassed;               // Time since this arrow was spawned
    float timeBeforeDestroyed = 2;  // Time after which this arrow will be destroyed
    public GameObject murderer;

    void Start() {
        murderer = Squeler.murderer;
    }
	void Update () {

        timePassed += Time.deltaTime;
        if(timePassed>timeBeforeDestroyed)
        {
            //Destroy arrow
            Destroy(gameObject);
        }  
        
        Vector3 Arrow1Dir = murderer.transform.position - transform.position;
        float Arrow1Angle = Mathf.Atan2(Arrow1Dir.y, Arrow1Dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.AngleAxis(Arrow1Angle + 180, Vector3.forward);
    }
}
