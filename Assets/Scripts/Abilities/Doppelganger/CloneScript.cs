using UnityEngine;
using System.Collections;

public class CloneScript : MonoBehaviour {

    public float timeBeforeDestroyed;   // Time after which this clone self-destructs
    public float timePassed;            // Time since this clone was summoned
    public Vector2 direction;           // Direction in which this clone will be running to
    public float cloneSpeed;            // Speed of the clone
    public Vector3 Destination;
    bool destroy = false;
    void Start()
    {
        timePassed = 0;
    }

	void Update () {
        if(gameObject.transform.position.x>Destination.x && direction == new Vector2(1,0))
        {
            cloneSpeed = 0;
            destroy = true;
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Running", false);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Running", false);
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", true);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Idle", true);
        }
        if (gameObject.transform.position.x < Destination.x && direction == new Vector2(-1, 0))
        {
            cloneSpeed = 0;
            destroy = true;
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Running", false);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Running", false);
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", true);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Idle", true);
        }
        if (destroy)
        {
            timePassed += Time.deltaTime;
        }
       if(timePassed > timeBeforeDestroyed)
        {
            Destroy(gameObject);
        }

        transform.Translate(direction * Time.deltaTime * cloneSpeed);

    }
}
