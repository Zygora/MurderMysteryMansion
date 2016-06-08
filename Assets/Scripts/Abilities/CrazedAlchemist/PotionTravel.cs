using UnityEngine;
using System.Collections;

public class PotionTravel : MonoBehaviour
{
    public float potionSpeed;         // Speed with which potion moves  
    public Vector2 direction;         // Vector 2 that shows the direction potion will be moving
    public float rotationSpeed;       // Speed with with which potion will rotate
    float timePassed;                 // Time since the potion was trown
    public float timeBeforeDestroyed; // Time after which potion will be destroyed if it didn't hit anyone

    void Update()
    {
        // Count for how long was this potion "alive"
        timePassed += Time.deltaTime;
        // Rotate potion
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        // While it's moving in the chosen direction
        transform.Translate(direction * Time.deltaTime * potionSpeed, Space.World);
        // If potion was "alive" for more than it's "alive" time -> destroy it
        if (timePassed > timeBeforeDestroyed)
        {
            Destroy(gameObject);
        }
    }
}