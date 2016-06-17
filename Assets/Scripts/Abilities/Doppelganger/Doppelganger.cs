using UnityEngine;
using System.Collections;

public class Doppelganger : MonoBehaviour
{
    public int clonesAvailable;
    public float cooldown;
    public float arrowSpeed;
    public float timeBeforeDestroyed;
    public GameObject arrow;
    Vector3 CloneDestination;
    Vector2 direction;
    float timeSinceUsed;
    bool cloneUsed;
    bool down;
    bool hold;

    void Update()
    {
        if (cloneUsed)
        {
            timeSinceUsed += Time.deltaTime;
            if (timeSinceUsed > cooldown)
            {
                cloneUsed = false;
            }
        }
        if (!cloneUsed)
        {
            if (clonesAvailable > 0)
            {
                // right
                if (Input.GetKeyDown(KeyCode.E) && (gameObject.GetComponent<Controls>().direction > 0))
                {
                    arrow.SetActive(true);
                    direction = new Vector2(1, 0);
                    arrow.transform.position = gameObject.transform.position;
                    arrow.transform.position -= new Vector3(0, 1, 0);
                    down = true;
                }
                // left
                if (Input.GetKeyDown(KeyCode.E) && (gameObject.GetComponent<Controls>().direction < 0))
                {
                    arrow.SetActive(true);
                    direction = new Vector2(-1, 0);
                    arrow.transform.position = gameObject.transform.position;
                    arrow.transform.position -= new Vector3(0, 1, 0);
                    down = true;
                }

                if (Input.GetKey(KeyCode.E) && (down))
                {
                    arrow.transform.Translate(direction * Time.deltaTime * arrowSpeed);
                    CloneDestination = arrow.transform.position;
                    hold = true;
                }

                if (Input.GetKeyUp(KeyCode.E) && (down) && (hold))
                {
                    arrow.SetActive(false);
                    GameObject clone = Instantiate(Resources.Load("Clone")) as GameObject;
                    if (direction == new Vector2(-1, 0))
                    {
                        clone.transform.eulerAngles = new Vector3(0, 180, 0);
                        clone.GetComponent<CloneScript>().cloneSpeed *= -1;
                    }
                    clone.transform.position = gameObject.transform.position;
                    clone.GetComponent<CloneScript>().timeBeforeDestroyed = timeBeforeDestroyed;
                    clone.GetComponent<CloneScript>().direction = direction;
                    clone.GetComponent<CloneScript>().Destination = CloneDestination;
                    clone.GetComponent<Controls>().TorsoAnimator.SetBool("Running", true);
                    clone.GetComponent<Controls>().LegsAnimator.SetBool("Running", true);
                    clone.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", false);
                    clone.GetComponent<Controls>().LegsAnimator.SetBool("Idle", false);
                    Physics2D.IgnoreCollision(clone.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
                    clonesAvailable--;
                    cloneUsed = true;
                    timeSinceUsed = 0;
                    down = false;
                    hold = false;
                }
            }
        }
    }
}
