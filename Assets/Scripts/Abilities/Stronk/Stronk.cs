using UnityEngine;
using System.Collections;

public class Stronk : MonoBehaviour {
    public GameObject player;
    bool nearWimp;
    bool grabbed;
    public Vector2 throwDirectionRight;
    public Vector2 throwDirectionLeft;
    public float throwForce;

	// Update is called once per frame
	void Update () {

        if ((Input.GetKeyDown(KeyCode.E))&&(nearWimp))
        {
            player.GetComponent<Rigidbody2D>().isKinematic = true;
            grabbed = true;
            player.transform.position = new Vector3(
                gameObject.transform.position.x-0.3f, 
                gameObject.transform.position.y+3,
                gameObject.transform.position.z);
            player.transform.rotation = Quaternion.Euler(0, 0, 90);
            player.transform.parent = gameObject.transform;
        }
        if ((Input.GetKeyDown(KeyCode.R)) && (grabbed))
        { 
            player.transform.parent = null;
            player.GetComponent<Rigidbody2D>().isKinematic = false;
            if (gameObject.GetComponent<Controls>().direction < 0)
            {
                player.GetComponent<Rigidbody2D>().AddForce(throwDirectionLeft * throwForce, ForceMode2D.Impulse);
            }
            else
            {
                player.GetComponent<Rigidbody2D>().AddForce(throwDirectionRight * throwForce, ForceMode2D.Impulse);
            }
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            grabbed = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wimp")
        {
            nearWimp = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Wimp")
        {
            nearWimp = false;
        }
    }
}
