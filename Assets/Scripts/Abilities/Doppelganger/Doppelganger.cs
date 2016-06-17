using UnityEngine;
using System.Collections;

public class Doppelganger : MonoBehaviour
{
    public int clonesAvailable;      // The amount of clones available to player
    public float cooldown;           // Cooldown time player has to wait before he is able to summon another clone
    public float arrowSpeed;         // Speed with which arrow moves
    public float timeBeforeDestroyed;// Time after which a clone is destroyed after reaching the point
    public GameObject arrow;         // Arrow object (Has to be in the scene and disabled)
    Vector3 CloneDestination;        // Point to which clone should travel after a player releases the ability button
    Vector2 direction;               // Direction in which clone and arrow will be moving
    float timeSinceUsed;             // Time passed since the ability was last used
    bool cloneUsed;                  // Flag that shows that the ability was used
    bool down;                       // Flag that show that ability buttin was pressed
    bool hold;                       // Flag that show that ability buttin was holded (We need them so player cant just hold button through cooldown and release it after and activate clone without arrow)

    void Update()
    {
        // If clone used count time and enable to use the ability after cooldown time passed
        if (cloneUsed)
        {
            timeSinceUsed += Time.deltaTime;
            if (timeSinceUsed > cooldown)
            {
                cloneUsed = false;
            }
        }
        // If ability can be used
        if (!cloneUsed)
        {
            // If there's another clone charge available
            if (clonesAvailable > 0)
            {
                // Player faces right
                if (Input.GetKeyDown(KeyCode.E) && (gameObject.GetComponent<Controls>().direction > 0))
                {
                    arrow.SetActive(true);
                    direction = new Vector2(1, 0);
                    arrow.transform.position = gameObject.transform.position;
                    arrow.transform.position -= new Vector3(0, 1, 0);
                    down = true;
                }
                // Player faces left
                if (Input.GetKeyDown(KeyCode.E) && (gameObject.GetComponent<Controls>().direction < 0))
                {
                    arrow.SetActive(true);
                    direction = new Vector2(-1, 0);
                    arrow.transform.position = gameObject.transform.position;
                    arrow.transform.position -= new Vector3(0, 1, 0);
                    down = true;
                }
                // While player holds the ability button move the arrow and save the spot where it is
                if (Input.GetKey(KeyCode.E) && (down))
                {
                    arrow.transform.Translate(direction * Time.deltaTime * arrowSpeed);
                    CloneDestination = arrow.transform.position;
                    hold = true;
                }
                // After player releases the button
                if (Input.GetKeyUp(KeyCode.E) && (down) && (hold))
                {
                    // Disable the arrow
                    arrow.SetActive(false);
                    // Instantiate a clone
                    GameObject clone = Instantiate(Resources.Load("Clone")) as GameObject;
                    // If we need to flip the clone make it's speed negative so it will be moving in the right direction
                    if (direction == new Vector2(-1, 0))
                    {
                        clone.transform.eulerAngles = new Vector3(0, 180, 0);
                        clone.GetComponent<CloneScript>().cloneSpeed *= -1;
                    }
                    // Move clone to players posi tion
                    clone.transform.position = gameObject.transform.position;
                    // Pass the time after which it shoul be destroyed to the clone's script
                    clone.GetComponent<CloneScript>().timeBeforeDestroyed = timeBeforeDestroyed;
                    clone.GetComponent<CloneScript>().direction = direction;
                    clone.GetComponent<CloneScript>().Destination = CloneDestination;
                    // Activate running animations for the clone
                    clone.GetComponent<Controls>().TorsoAnimator.SetBool("Running", true);
                    clone.GetComponent<Controls>().LegsAnimator.SetBool("Running", true);
                    clone.GetComponent<Controls>().TorsoAnimator.SetBool("Idle", false);
                    clone.GetComponent<Controls>().LegsAnimator.SetBool("Idle", false);
                    // Ignore collision with other players
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
