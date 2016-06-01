using UnityEngine;
using System.Collections;

public class PotionTravel : MonoBehaviour {
    public float potionSpeed;
    public float potionDistance;
    public Vector2 direction;
    public float rotationSpeed;
    float timePassed;
    public float timeBeforeDestroyed;
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        transform.Translate(direction * Time.deltaTime * potionSpeed,Space.World);
        if(timePassed>timeBeforeDestroyed)
        {
            Destroy(gameObject);
        }
	}
}
