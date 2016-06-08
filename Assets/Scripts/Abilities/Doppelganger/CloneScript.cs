using UnityEngine;
using System.Collections;

public class CloneScript : MonoBehaviour {

    public float timeBeforeDestroyed;   // Time after which this clone self-destructs
    public float timePassed;            // Time since this clone was summoned
    public Vector2 direction;           // Direction in which this clone will be running to
    public float cloneSpeed;            // Speed of the clone

    void Start()
    {
        timePassed = 0;
    }

	void Update () {
        timePassed += Time.deltaTime;
        transform.Translate(direction * Time.deltaTime * cloneSpeed);
        if (timePassed > timeBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
