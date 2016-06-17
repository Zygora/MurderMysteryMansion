using UnityEngine;
using System.Collections;

public class ArowScript : MonoBehaviour {
    public Vector2 direction = new Vector2(1,0);           // Direction in which this arrow will be running to
    public float arrowSpeed = 3.5f;            // Speed of the arrow
    public bool hold = true;

	// Update is called once per frame
	void Update () {
        if (hold)
        {
            transform.Translate(direction * Time.deltaTime * arrowSpeed);
        }
    }
}
