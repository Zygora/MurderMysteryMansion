using UnityEngine;
using System.Collections;

public class MurdererScripts : MonoBehaviour {
    Animator Animator;
    public static bool isMurderer;
    public GameObject shirt1;
    public GameObject shirt2;
    public GameObject shirt3;
    public GameObject shirt4;

    // Use this for initialization
    void Start () {
        isMurderer = true;
        Animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > -0.5f && Input.GetAxis("Horizontal") < 0.5f)
        {
            Animator.SetBool("MurdererRunning", false);
            Animator.SetBool("Idle", true);
        }

        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            Animator.SetBool("MurdererRunning", true);
            Animator.SetBool("Idle", false);
            GetComponent<SpriteRenderer>().flipX = true;
            shirt1.transform.localRotation = Quaternion.Euler(0, 180, 0);
            shirt1.transform.position = new Vector3(this.transform.position.x + -.05f, shirt1.transform.position.y, 0);
            shirt2.transform.localRotation = Quaternion.Euler(0, 180, 0);
            shirt2.transform.position = new Vector3(this.transform.position.x + .02f, shirt1.transform.position.y, 0);
            shirt3.transform.localRotation = Quaternion.Euler(0, 180, 0);
            shirt3.transform.position = new Vector3(this.transform.position.x + -.05f, shirt1.transform.position.y, 0);
            shirt4.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            Animator.SetBool("MurdererRunning", true);
            Animator.SetBool("Idle", false);
            GetComponent<SpriteRenderer>().flipX = false;
            shirt1.transform.localRotation = Quaternion.Euler(0, 0, 0);
            shirt1.transform.position = new Vector3(this.transform.position.x + .05f, shirt1.transform.position.y, 0);
            shirt2.transform.localRotation = Quaternion.Euler(0, 0, 0);
            shirt2.transform.position = new Vector3(this.transform.position.x + -.02f, shirt1.transform.position.y, 0);
            shirt3.transform.localRotation = Quaternion.Euler(0, 0, 0);
            shirt3.transform.position = new Vector3(this.transform.position.x + .05f, shirt1.transform.position.y, 0);
            shirt4.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
