using UnityEngine;
using System.Collections;

public class CloneScript : MonoBehaviour
{
    public Vector2 direction;           // Direction in which this clone will be running to

    public float cloneSpeed;            // Speed of the clone

    public Vector3 startPosition;

    void Start()
    {
     
    }

    void Update()
    {
        //move clone and set animations of clone
        if (Doppelganger.cloneMoving==true)
        {
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Running", true);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Running", true);
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", false);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Idle", false);
            transform.Translate(direction * Time.deltaTime * cloneSpeed);
        }

        //stop clone from moving, change animations and destroy clone after 2 seconds
        if (Doppelganger.cloneMoving == false)
        {
            // After clone reaches the destination change nimation to idle
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Running", false);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Running", false);
            gameObject.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", true);
            gameObject.GetComponent<Controls>().LegsAnimator.SetBool("Idle", true);
            Invoke("DestroyClone", 2f);
        }

        //stop clone from moving after it reaches x distance from the player
        if (this.transform.position.x >= startPosition.x + 100 || this.transform.position.x <= startPosition.x - 100 ||
            transform.position.x >= 570 || transform.position.x <= -570)
        {
            Doppelganger.cloneMoving = false;
        }
    }

    //FUNCTIONS
    void DestroyClone() {
        Destroy(this.gameObject);
        Doppelganger.cloneAlive = false;
    }
}
